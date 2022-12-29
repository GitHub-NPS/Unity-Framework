using BayatGames.SaveGameFree;
using BayatGames.SaveGameFree.Serializers;
using UnityEngine;
using com.unimob.pattern.singleton;
using NPS.Pattern.Observer;
using System.Collections.Generic;

public class DatasaveManager : MonoSingleton<DatasaveManager>
{
    private List<ADataSave> saves = new List<ADataSave>();

    #region General
    public GeneralSave General
    {
        get => general;
        set { general = value; }
    }

    [SerializeField] private GeneralSave general;
    #endregion

    #region User
    public UserSave User
    {
        get => user;
        set { user = value; }
    }

    [SerializeField] private UserSave user;
    #endregion

    #region RemoteConfig
    public RemoteConfigSave RemoteConfig
    {
        get => remoteConfig;
        set { remoteConfig = value; }
    }

    [SerializeField] private RemoteConfigSave remoteConfig;
    #endregion

    #region Tutorial
    public TutorialSave Tutorial
    {
        get => tutorial;
        set { tutorial = value; }
    }

    [SerializeField] private TutorialSave tutorial;
    #endregion

    #region Time
    public TimeSave Time
    {
        get => time;
        set { time = value; }
    }

    [SerializeField] private TimeSave time;
    #endregion

    private bool encode = true;
    private string password = "NPS";

    public void Init(Transform parent = null)
    {
        DataManager.Save = this;
        if (parent) transform.SetParent(parent);

        SaveGame.Encode = encode;
        SaveGame.EncodePassword = password;
        SaveGame.Serializer = new SaveGameJsonSerializer();
        LoadData();
        FixData();

        this.PostEvent(EventID.LoadSuccess);
    }

    public void FixData()
    {
        foreach (var save in saves)
            save.Fix();

#if DEVELOPMENT
        if ((UnbiasedTime.UtcNow - Time.LastTimeOut).TotalMinutes > 30)
            NextDay();
#else
        if ((UnbiasedTime.UtcNow - Time.LastTimeOut).Days != 0)
            NextDay();
#endif
    }

    private void NextDay()
    {
        Time.SetLastTimeOut();
    }

    private void OnApplicationPause(bool pause)
    {
        if (pause)
        {
            Time.SetLastTimeOut();
            General.Save();
        }
    }

    private void OnApplicationQuit()
    {
        SaveData();
    }

    private void SaveData()
    {
        Time.SetLastTimeOut();

        foreach (var save in saves)
            save.Save();
    }

    public void LoadData()
    {
        saves.Clear();

        remoteConfig = LoadData(new RemoteConfigSave("RemoteConfig"));

        general = LoadData(new GeneralSave("General"));

        time = LoadData(new TimeSave("Time"));

        tutorial = LoadData(new TutorialSave("Tutorial"));

        user = LoadData(new UserSave("User"));
    }

    private T LoadData<T>(T def) where T : ADataSave
    {
        var rs = SaveGame.Load(def.Key, def);
        saves.Add(rs);
        return rs;
    }

    public void ClearData()
    {
        SaveGame.Clear();
    }
}
