using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NPS;

public class AppManager : MonoSingleton<AppManager>
{
    public static AppsFlyerManager AppsFlyer;
    public static FirebaseManager Firebase;
    public static AdsManager Ads;
    public static RateManager Rate;
    public static NotificationManager Notification;
    public static IAPManager Iap;
    public static LoginManager Login;
    public static FacebookManager Facebook;

    public void Init()
    {
        FacebookManager.S.Init(transform);
        AppsFlyerManager.S.Init(transform);
        FirebaseManager.S.Init(transform);
        AdsManager.S.Init(transform);
        RateManager.S.Init(transform);
        NotificationManager.S.Init(transform);
        IAPManager.S.Init(transform);
        LoginManager.S.Init(transform);
    }
}
