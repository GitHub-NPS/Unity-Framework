using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using com.unimob.timer;

public class UIOfflineReward : UIView
{
    [SerializeField] private GameObject btnLoading;

    private TimeSave timeSave;
    private TickData tick = new TickData();

    protected override void Init()
    {
        base.Init();

        timeSave = DataManager.Save.Time;
    }    

    public override void Show(object obj = null)
    {
        base.Show(obj);

        CheckButtonLoading();
    }

    private void CheckButtonLoading()
    {
        btnLoading.SetActive(DataManager.Save.General.IsShowRewardAds);
        if (btnLoading.activeSelf)
        {
            tick.Action = () =>
            {
                if (AppManager.Ads.IsRewardedAdReady())
                {
                    btnLoading.SetActive(false);
                    tick.RemoveTick();
                }
            };
            tick.RegisterTick();
        }
    }

    public override void Hide()
    {
        base.Hide();

        tick.RemoveTick();

        timeSave.SetLastTimeOut();
        timeSave.Save();

        MainGameScene.S.ShowStartPopup();
    }
}
