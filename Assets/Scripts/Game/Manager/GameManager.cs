using System;
using System.Collections;
using System.Collections.Generic;
using com.unimob.mec;
using com.unimob.timer;
using DG.Tweening;
using NPS.Pattern.Observer;
using Sirenix.OdinInspector;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager S;

    [SerializeField] private UIWaitting Waitting;

#if UNITY_EDITOR || DEVELOPMENT
    public string ID = "test0";
    public string Link = "";
#endif
    public bool isCloud = true;

    public bool InBlackList = false;
    public string DeviceId = string.Empty;

    private LoginData login;

    private void Awake()
    {
        if (!S) S = this;

#if !UNITY_EDITOR
        DeviceId = DeviceHelper.GetDeviceId();
        Debug.Log(DeviceId);

        InBlackList = DeviceId.InBlacklist();
#endif        

        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        Application.targetFrameRate = 120;
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");

        DontDestroyOnLoad(this);

#if DEVELOPMENT || UNITY_EDITOR || STAGING
        if (Application.isEditor)
            Application.runInBackground = true;
#endif
    }

    private void Start()
    {
        DataManager.S.Init();
        AppManager.S.Init();

        if (isCloud)
        {
            if (!DataManager.Save.User.Logged)
            {
                AppManager.Login.OnLogin += OnLoginHandler;
                AppManager.Login.Login();
            }
            else GetVersion();
        }
    }

    private void OnLoginHandler(LoginData data)
    {
        AppManager.Login.OnLogin -= OnLoginHandler;

        login = data;
        GetVersion();
    }

    private void GetVersion()
    {
#if ANDROID_FREE_PRODUCTION || IOS_FREE_PRODUCTION
        if (Waitting) Waitting.Show(-1f);

        AppManager.Cloud.OnGetVersionCloud += OnGetVersionHandle;
        AppManager.Cloud.GetVersion();
#endif
    }

    private void OnGetVersionHandle(VersionCloud cloudData)
    {
        AppManager.Cloud.OnGetVersionCloud -= OnGetVersionHandle;

        if (Waitting) Waitting.Hide();
        if (cloudData != null)
        {
            if (Waitting) Waitting.Show(-1f);

            AppManager.Cloud.OnGetUserDataCloud += OnGetUserDataCloud;
            AppManager.Cloud.GetUserData();
        }
    }

    private void OnGetUserDataCloud(UserDataCloud cloudData)
    {
        AppManager.Cloud.OnGetUserDataCloud -= OnGetUserDataCloud;

        if (Waitting) Waitting.Hide();
        if (cloudData != null)
        {
            DataManager.Save.OverwriteCurrentUserData(cloudData);

            if (login != null)
                AppManager.Login.SetDataLogin(login);

            Active();
        }

        AppManager.Cloud.CheckPostData();
    }

    private void Active()
    {
        var userSave = DataManager.Save.User;

        KillAll();

        MonoScene.S.Load("Main");
    }

    private void OnDestroy()
    {
        KillAll();

        AppManager.Login.OnLogin -= OnLoginHandler;
        AppManager.Cloud.OnGetVersionCloud -= OnGetVersionHandle;
        AppManager.Cloud.OnGetUserDataCloud -= OnGetUserDataCloud;
    }

    public void KillAll()
    {
        Debug.Log("Kill All");

        TimerManager.S?.Clear();
        Observer.S?.ClearAllListener();
        TutorialManager.S?.Clear();
        Timing.KillCoroutines();
        DOTween.KillAll();
    }

#if UNITY_EDITOR
    [Button]
    public void GetUserData(string uId)
    {
        var userSave = DataManager.Save.User;
        userSave.uId = uId;
        userSave.CloudVersion = -1;

        AppManager.Cloud.OnGetUserDataCloud += OnGetUserDataCloud;
        AppManager.Cloud.GetUserData();
    }

    [Button]
    public void PostUserData()
    {
        AppManager.Cloud.PostUserData();
    }
#endif
}