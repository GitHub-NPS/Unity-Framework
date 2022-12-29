using System.Collections;
using UnityEngine;
using com.unimob.pattern.singleton;
using System;
using System.Collections.Generic;

#if UNITY_ADJUST
using com.adjust.sdk;
#endif

public class AdjustManager : MonoSingleton<AdjustManager>
{
    public void Init(Transform parent = null)
    {
        AppManager.Adjust = this;
        if (parent) transform.SetParent(parent);

#if UNITY_IOS
        /* Mandatory - set your iOS app token here */
        InitAdjust("YOUR_IOS_APP_TOKEN_HERE");
#elif UNITY_ANDROID
        /* Mandatory - set your Android app token here */
        InitAdjust("808vugvpdqm8");
#endif
    }

    private void InitAdjust(string adjustAppToken)
    {
#if UNITY_ADJUST
        var adjustConfig = new AdjustConfig(
            adjustAppToken,
            AdjustEnvironment.Production, // AdjustEnvironment.Sandbox to test in dashboard
            true
        );
        adjustConfig.setLogLevel(AdjustLogLevel.Info); // AdjustLogLevel.Suppress to disable logs
        adjustConfig.setSendInBackground(true);
        new GameObject("Adjust").AddComponent<Adjust>(); // do not remove or rename
        // Adjust.addSessionCallbackParameter("foo", "bar"); // if requested to set session-level parameters
        //adjustConfig.setAttributionChangedDelegate((adjustAttribution) => {
        //  Debug.LogFormat("Adjust Attribution Callback: ", adjustAttribution.trackerName);
        //});
        Adjust.start(adjustConfig);
#endif
    }

    public void RevenueTracking(decimal priceAmount, string isoCurrencyCode, string transactionId)
    {
#if UNITY_ADJUST
        var adjustEvent = new AdjustEvent("ij3dnp");
#if UNITY_ANDROID
        adjustEvent.setRevenue(decimal.ToDouble(priceAmount) * 0.85f, isoCurrencyCode);
#elif UNITY_IOS
        adjustEvent.setRevenue(decimal.ToDouble(priceAmount) * 0.7f, isoCurrencyCode);
#endif
        adjustEvent.setTransactionId(transactionId);
        Adjust.trackEvent(adjustEvent);
#endif
    }

#if UNITY_IRONSOURCE
    public void AdImpression(IronSourceImpressionData impressionData)
    {
#if UNITY_ADJUST
        var adjustAdRevenue = new AdjustAdRevenue(AdjustConfig.AdjustAdRevenueSourceIronSource);
        adjustAdRevenue.setRevenue(impressionData.revenue ?? 0, "USD");
        adjustAdRevenue.setAdRevenueNetwork(impressionData.adNetwork);
        adjustAdRevenue.setAdRevenueUnit(impressionData.adUnit);
        adjustAdRevenue.setAdRevenuePlacement(impressionData.placement);
        Adjust.trackAdRevenue(adjustAdRevenue);
#endif
    }
#endif
}