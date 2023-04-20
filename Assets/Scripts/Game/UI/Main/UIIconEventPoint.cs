using NPS.Pattern.Observer;
using TMPro;
using UnityEngine;
using NPS.Math;

public class UIIconEventPoint : UIIconEventItem
{
    [SerializeField] private TextMeshProUGUI txtPoint;

    private UserSave userSave;

    public override void Set()
    {
        userSave = DataManager.Save.User;

        txtPoint.text = userSave.Currency[CurrencyType.Point].Show();

        this.RegisterListener(EventID.ChangeCurrency, OnChangeCurrency);
        
        base.Set();
    }

    private void OnChangeCurrency(object obj)
    {
        CurrencyType type = (CurrencyType)obj;
        if (type == CurrencyType.Point)
        {
            txtPoint.text = userSave.Currency[CurrencyType.Point].Show();
        }
    }

    private void OnDestroy()
    {
        this.RemoveListener(EventID.ChangeCurrency, OnChangeCurrency);
    }

    public override void OnClick()
    {
        MainGameScene.S.Show<UIEventNoel>();
    }
}