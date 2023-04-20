using System;
using System.Collections;
using System.Collections.Generic;
using com.unimob.mec;
using com.unimob.timer;
using DG.Tweening;
using NPS.Pattern.Observer;
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

    private LoginData login;

    private void Awake()
    {
        if (!S) S = this;

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
        else Active();
    }

    private void OnLoginHandler(LoginData data)
    {
        login = data;
        GetVersion();
    }

    private void GetVersion()
    {
#if ANDROID_FREE_PRODUCTION
        if (Waitting) Waitting.Show(-1f);

        AppManager.Cloud.OnGetVersionCloud += OnGetVersionHandle;
        AppManager.Cloud.GetVersion();
#endif
    }

    private void OnGetVersionHandle(VersionCloud cloudData)
    {
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
        if (Waitting) Waitting.Hide();
        if (cloudData != null)
        {
            DataManager.Save.OverwriteCurrentUserData(cloudData);

            if (login != null)
                AppManager.Login.SetDataLogin(login);

            Active();
        }
    }

    private void Active()
    {
        var userSave = DataManager.Save.User;

        TimerManager.S?.Clear();
        Observer.S?.ClearAllListener();
        Timing.KillCoroutines();
        DOTween.KillAll();
        TutorialManager.S.Clear();

        MonoScene.S.Load("Main");
    }

    private void OnDestroy()
    {
        AppManager.Login.OnLogin -= OnLoginHandler;
        AppManager.Cloud.OnGetVersionCloud -= OnGetVersionHandle;
        AppManager.Cloud.OnGetUserDataCloud -= OnGetUserDataCloud;
    }
}