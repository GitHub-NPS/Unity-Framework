using NPS;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NPS.Loot;

public class UIItemCurrency : UIItem
{
    public override void Set(object data)
    {
        var item = data as CurrencyData;

        imgIcon.sprite = ResourceManager.S.LoadSprite("Currency", item.Type.ToString());
        imgFrame.sprite = ResourceManager.S.LoadSprite("FrameCurrency", item.Type.ToString());
        txtAmount.text = item.Value.Show();
    }
}
