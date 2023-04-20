using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using com.unimob.pattern.singleton;

#if UNITY_APPSFLYER
using AppsFlyerSDK;
#endif

public class AppsFlyerManager : MonoSingleton<AppsFlyerManager>
{
    private string devKey = "HWAAfxs6ec2wwZfsRpjipJ";

    private string androidPublicKey = "MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEApCeSSfGeB/YmRP5V8KHqyb19C6e9M4S2AeNpVcIylOpgSTIeTImzjz0RCXnYjnGZrkuruO9Fgn/FcLPN1015JUNmEUOU1p6R0pL+ugvkuKeP9xQf1s8QrCToENS0EnHiC3fJOcUi3za8XKKqt2Tn1pqyua5l2yQt0TTN5HuK/QzSV4zDE787UXeaEfh7sxd5+pOfVRkpXWcdWdbzecffRvTs7CwWiPZ3mqDM5ADjp/gmT81sfSI21h7fKOkHFRKP5jM7mNsm/Nqnq6v5GE4pdC+lndxtAbn2+dUQcKHb7QzLqDd/aDEguRw4mB3CcSsplL0m6ONqyj49fGXUwyx43QIDAQAB";
    private string iOSAppId = "6447154983";
    private bool m_IsInitialized = false;

    private bool tokenSent;

    private void Awake()
    {
#if ANDROID_FREE_PRODUCTION
#if ABI_PUBLISH
        devKey = "G3MBmMRHTuEpXbqyqSWGeK";
#endif
        devKey = "HWAAfxs6ec2wwZfsRpjipJ";
#elif IOS_FREE_PRODUCTION
        devKey = "HWAAfxs6ec2wwZfsRpjipJ";
        iOSAppId = "6447154983";
#else
        devKey = "HWAAfxs6ec2wwZfsRpjipJ";
        iOSAppId = "6447154983";
#endif
    }

    private void Start()
    {
#if UNITY_APPSFLYER
        AppsFlyer.setIsDebug(false);
        AppsFlyer.initSDK(devKey, iOSAppId);
        AppsFlyer.startSDK();
        m_IsInitialized = true;

#if UNITY_IOS
        UnityEngine.iOS.NotificationServices.RegisterForNotifications(UnityEngine.iOS.NotificationType.Alert | UnityEngine.iOS.NotificationType.Badge | UnityEngine.iOS.NotificationType.Sound);
#endif
#endif
    }

    public void Init(Transform parent = null)
    {
        AppManager.AppsFlyer = this;
        if (parent) transform.SetParent(parent);
    }

    private void Update()
    {
#if UNITY_APPSFLYER
#if UNITY_IOS
        if (!tokenSent)
        {
            byte[] token = UnityEngine.iOS.NotificationServices.deviceToken;
            if (token != null)
            {
                var app = AppsFlyer.instance is AppsFlyeriOS ? AppsFlyer.instance as AppsFlyeriOS : null;
                if (app != null)
                {
                    app.registerUninstall(token);
                    tokenSent = true;
                }
            }
        }
#endif
#endif
    }

    public void AdImpression(string ad_platform, string ad_source, string ad_unit_name, string ad_format, string ad_revenue)
    {
#if UNITY_APPSFLYER
        if (m_IsInitialized)
        {
            var eventParams = new Dictionary<string, string>();
            eventParams.Add("af_platform", ad_platform);
            eventParams.Add("af_source", ad_source);
            eventParams.Add("af_format", ad_format);
            eventParams.Add("af_currency", "USD");
            eventParams.Add("af_value", ad_revenue);

            if (ad_unit_name.Equals("rewarded_video"))
            {
                AppsFlyer.sendEvent("af_impression_rewarded", eventParams);
            }
            else if (ad_unit_name.Equals("interstitial"))
            {
                AppsFlyer.sendEvent("af_impression_interstitial", eventParams);
            }
            else if (ad_unit_name.Equals("banner"))
            {
                AppsFlyer.sendEvent("af_impression_banner", eventParams);
            }
        }
#endif
    }

    public void InterstitialAdEligibleTracking()
    {
#if UNITY_APPSFLYER
        //AppsFlyer.sendEvent("af_inters_ad_eligible", null);

        var general = DataManager.Save.General;
        general.CountInterAds++;
        if (general.CountInterAds < 9) general.Save();

        if (3 <= general.CountInterAds && general.CountInterAds <= 9)
        {


            var eventParams = new Dictionary<string, string>();
            eventParams.Add("af_count", general.CountInterAds.ToString());

            AppsFlyer.sendEvent("af_ad_inters", eventParams);
        }
#endif
    }

    public void InterstitialAdReadyTracking()
    {
#if UNITY_APPSFLYER
        AppsFlyer.sendEvent("af_inters_api_called", null);
#endif
    }

    public void InterstitialAdOpenTracking()
    {
#if UNITY_APPSFLYER
        AppsFlyer.sendEvent("af_inters_displayed", null);
#endif
    }

    public void RewardedAdEligibleTracking()
    {
#if UNITY_APPSFLYER
        //AppsFlyer.sendEvent("af_rewarded_ad_eligible", null);

        var general = DataManager.Save.General;
        general.CountRewardAds++;
        if (general.CountRewardAds < 5) general.Save();

        if (1 <= general.CountRewardAds && general.CountRewardAds <= 5)
        {
            var eventParams = new Dictionary<string, string>();
            eventParams.Add("af_count", general.CountRewardAds.ToString());

            AppsFlyer.sendEvent("af_ad_reward", eventParams);
        }
#endif
    }

    public void RewardedAdReadyTracking()
    {
#if UNITY_APPSFLYER
        AppsFlyer.sendEvent("af_rewarded_api_called", null);
#endif
    }

    public void RewardedAdOpenTracking()
    {
#if UNITY_APPSFLYER
        AppsFlyer.sendEvent("af_rewarded_ad_displayed", null);
#endif
    }

    public void UninstallTracking(string token)
    {
#if UNITY_APPSFLYER
#if UNITY_ANDROID
        if (AppsFlyer.instance != null) (AppsFlyer.instance as IAppsFlyerAndroidBridge).updateServerUninstallToken(token);

#endif
#endif
    }

    public void AndroidRevenueTracking(string signature, string purchaseData, string price, string currency)
    {
#if UNITY_APPSFLYER
#if UNITY_ANDROID
        if (AppsFlyer.instance != null) (AppsFlyer.instance as IAppsFlyerAndroidBridge).validateAndSendInAppPurchase(androidPublicKey, signature, purchaseData, price, currency, null, this);
#endif
#endif
    }

    public void iOSRevenueTracking(string prodId, string price, string currency, string transactionId)
    {
#if UNITY_APPSFLYER
#if UNITY_IOS
        if (AppsFlyer.instance != null) (AppsFlyer.instance as IAppsFlyerIOSBridge).validateAndSendInAppPurchase(prodId, price, currency, transactionId, null, this);
#endif
#endif
    }

    public void CompleteTutorialTracking(bool af_success, int af_tutorial_id, string af_content)
    {
#if UNITY_APPSFLYER
        var eventParams = new Dictionary<string, string>();
        eventParams.Add("af_success", af_success ? "true" : "false");
        eventParams.Add("af_tutorial_id", af_tutorial_id.ToString());
        eventParams.Add("af_content", af_content);

        AppsFlyer.sendEvent("af_tutorial_completion", eventParams);
#endif
    }

    public void LoginSuccessTracking()
    {
#if UNITY_APPSFLYER
        AppsFlyer.sendEvent("af_login", null);
#endif
    }
}
