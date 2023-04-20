using System;
using TMPro;
using UnityEngine;
using NPS.Math;
using NPS.Loot;
using UnityEngine.Serialization;
using UnityEngine.UI;
using com.unimob.timer;

public class UIOfflineReward : UIView
{
    [SerializeField] private TextMeshProUGUI txtTime;
    [SerializeField] private GameObject btnLoading;

    [SerializeField] private Image Icon;

    private TimeSave timeSave;
    private double currency;
    private CurrencyType type;

    private int mul = 1;

    protected override void Init()
    {
        base.Init();

        type = CurrencyType.Coin;
        timeSave = DataManager.Save.Time;

        Icon.sprite = ResourceManager.S.LoadSprite("OfflineReward", Utils.GetCurrency());        
    }    

    public override void Show(object obj = null)
    {
        base.Show(obj);

        Tuple<double, TimeSpan, TimeSpan> tuple = (Tuple<double, TimeSpan, TimeSpan>)obj;

        currency = tuple.Item1;
        var time = tuple.Item2;
        var sum = tuple.Item3;

        mul = 1;
        
        txtTime.text = I2.Loc.LocalizationManager.GetTranslation("You are away for:") + time.Show() + "/" + sum.Show();

        CheckButtonLoading();
    }

    private TickData tick = new TickData();

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
        MainGameScene.S.Loot.Loot(new LootData()
        {
            Type = LootType.Currency,
            Data = new CurrencyData()
            {
                Type = type,
                Value = currency * mul
            }
        });

        AudioManager.S.PlayOneShoot("Expense_Coin");

        MainGameScene.S.ShowStartPopup();
    }

    public void Double()
    {
        AppManager.Ads.ShowRewardedAd((result) =>
        {
            if (result)
            {
                mul = 2;
                Hide();
            }
            else
            {
                MainGameScene.S.Toast.Show(I2.Loc.LocalizationManager.GetTranslation("Ads unavailable"));
            }
        }, AdPlacement.RDemo);
    }
}
