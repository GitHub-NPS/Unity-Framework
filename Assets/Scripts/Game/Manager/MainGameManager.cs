using com.unimob.mec;
using System;
using UnityEngine;
using NPS.Pattern.Observer;

public class MainGameManager : MonoBehaviour
{
    public static MainGameManager S;

    private GeneralSave generalSave;

    private void Awake()
    {
        if (!S) S = this;

        this.RegisterListener(EventID.ChangeAds, OnChangeAds);
    }

    private void OnChangeAds(object obj)
    {
        if (!generalSave.Ads) AppManager.Ads.DestroyBanner();
        else AppManager.Ads?.HideBanner();
    }

    private void OnDestroy()
    {
        Timing.KillCoroutines();

        this.RemoveListener(EventID.ChangeAds, OnChangeAds);
    }

    private void Start()
    {
        generalSave = DataManager.Save.General;
        if (generalSave.Ads) AppManager.Ads?.ShowBanner();
    }
}
