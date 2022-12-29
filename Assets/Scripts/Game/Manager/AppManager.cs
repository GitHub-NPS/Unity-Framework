using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using com.unimob.pattern.singleton;

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
    public static ApiManager Api;
    public static AdjustManager Adjust;

    public void Init()
    {
        TrackingEvent.StartInit();
        AppsFlyerManager.S.Init(transform);
        FirebaseManager.S.Init(transform);
        AdsManager.S.Init(transform);
        RateManager.S.Init(transform);
        NotificationManager.S.Init(transform);
        IAPManager.S.Init(transform);
        LoginManager.S.Init(transform);
        FacebookManager.S.Init(transform);
        ApiManager.S.Init(transform);
        AdjustManager.S.Init(transform);
    }
}
