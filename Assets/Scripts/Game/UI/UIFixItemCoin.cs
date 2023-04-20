using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIFixItemCoin : UIFixItem
{
    public override void OnClick()
    {
#if DEVELOPMENT || UNITY_EDITOR || STAGING
        double currency = DataManager.Save.User.Currency[type];
        if (currency <= 0) currency = 100;
        DataManager.Save.User.IncreaseCurrency(type, currency);
#endif
    }
}
