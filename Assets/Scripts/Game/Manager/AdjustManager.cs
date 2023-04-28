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

        if (GameManager.S.InBlackList) return;

#if UNITY_IOS
        InitAdjust("808vugvpdqm8");
#elif UNITY_ANDROID
        InitAdjust("808vugvpdqm8");
#endif
    }

    private void InitAdjust(string adjustAppToken)
    {
#if UNITY_ADJUST
        var adjustConfig = new AdjustConfig(adjustAppToken, AdjustEnvironment.Production);
        Adjust.start(adjustConfig);
#endif
    }

    public void RevenueTracking(decimal priceAmount, string isoCurrencyCode, string transactionId)
    {
        if (GameManager.S.InBlackList) return;

#if UNITY_ADJUST
        var adjustEvent = new AdjustEvent("ij3dnp");
#if UNITY_ANDROID
        adjustEvent.setRevenue(decimal.ToDouble(priceAmount) * 0.75f, isoCurrencyCode);
#elif UNITY_IOS
        adjustEvent.setRevenue(decimal.ToDouble(priceAmount) * 0.85f, isoCurrencyCode);
#endif
        adjustEvent.setTransactionId(transactionId);
        Adjust.trackEvent(adjustEvent);
#endif
    }

    public void TrackingAdImpression(string adSource, string adUnitName, string adPlacement, double adRevenue)
    {
#if UNITY_ADJUST
        var adjustAdRevenue = new AdjustAdRevenue(AdjustConfig.AdjustAdRevenueSourceIronSource);
        adjustAdRevenue.setRevenue(adRevenue, "USD");
        adjustAdRevenue.setAdRevenueNetwork(adSource);
        adjustAdRevenue.setAdRevenueUnit(adUnitName);
        adjustAdRevenue.setAdRevenuePlacement(adPlacement);
        Adjust.trackAdRevenue(adjustAdRevenue);
#endif
    }
}