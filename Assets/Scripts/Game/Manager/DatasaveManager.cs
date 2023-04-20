using BayatGames.SaveGameFree;
using BayatGames.SaveGameFree.Serializers;
using UnityEngine;
using com.unimob.pattern.singleton;
using NPS.Pattern.Observer;
using System.Collections.Generic;
using System;

public class DatasaveManager : MonoSingleton<DatasaveManager>
{
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
        set
        {
            time = value;
        }
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
    }

    public void FixData()
    {
        bool isNextDay = (UnbiasedTime.UtcNow - General.CheckInTime).Days != 0;

        remoteConfig.Fix();

        general.Fix();

        time.Fix();

        tutorial.Fix();

        user.Fix();

        if (isNextDay) NextDay();
    }

    public void SaveData()
    {
        remoteConfig.Save();

        general.Save();

        time.Save();

        tutorial.Save();

        user.Save();
    }

    private void NextDay()
    {
        
    }

    private void OnApplicationPause(bool pause)
    {
        if (pause)
        {
            Time.SetLastTimeOut();
            Time.Save();
        }
    }

    public void LoadData()
    {
        general = SaveGame.Load("General", new GeneralSave("General"));

        time = SaveGame.Load("Time", new TimeSave("Time"));

        tutorial = SaveGame.Load("Tutorial", new TutorialSave("Tutorial"));

        user = SaveGame.Load("User", new UserSave("User"));

        remoteConfig = SaveGame.Load("RemoteConfig", new RemoteConfigSave("RemoteConfig"));
    }

    public void ClearData()
    {
        SaveGame.Clear();
    }

    public UserDataCloud GetNewUserDataCloud()
    {
        return new UserDataCloud
        {
            General = GetFileRawData(general.Key),

            Time = GetFileRawData(time.Key),

            Tutorial = GetFileRawData(tutorial.Key),

            User = GetFileRawData(user.Key),

            RemoteConfig = GetFileRawData(remoteConfig.Key),
        };
    }

    private string GetFileRawData(string fileName)
    {
        try
        {
            if (SaveGame.Exists(fileName))
            {
                var filePath = $"{Application.persistentDataPath}/Save/{fileName}";
                var data = System.IO.File.ReadAllText(filePath, SaveGame.DefaultEncoding);
                if (encode)
                {
                    var result = "";
                    var decoded = SaveGame.Encoder.Decode(data, password);
                    var stream = new System.IO.MemoryStream(Convert.FromBase64String(decoded), true);
                    using (var reader = new System.IO.StreamReader(stream, SaveGame.DefaultEncoding))
                    {
                        result = reader.ReadToEnd();
                    }

                    stream.Dispose();
                    return result;
                }

                return data;
            }

            return string.Empty;
        }
        catch (Exception e)
        {
            Debug.LogError($"Get file raw data Failed {e.Message} {e.StackTrace}\n{fileName}");
            return string.Empty;
        }
    }

    public void OverwriteCurrentUserData(UserDataCloud userData)
    {
        if (!string.IsNullOrEmpty(userData.General))
            general = LoadFromRawData<GeneralSave>(userData.General);

        if (!string.IsNullOrEmpty(userData.Time))
            time = LoadFromRawData<TimeSave>(userData.Time);

        if (!string.IsNullOrEmpty(userData.Tutorial))
            tutorial = LoadFromRawData<TutorialSave>(userData.Tutorial);

        if (!string.IsNullOrEmpty(userData.RemoteConfig))
            remoteConfig = LoadFromRawData<RemoteConfigSave>(userData.RemoteConfig);

        FixData();
        SaveData();
    }

    private T LoadFromRawData<T>(string rawData)
    {
        try
        {
            var stream = new System.IO.MemoryStream(SaveGame.DefaultEncoding.GetBytes(rawData));
            var saveObj = SaveGame.Serializer.Deserialize<T>(stream, SaveGame.DefaultEncoding);
            stream.Dispose();
            return saveObj;
        }
        catch (Exception e)
        {
            Debug.LogError($"Load raw data Failed {e.Message} {e.StackTrace}\n{rawData}");
            return default(T);
        }
    }
}