using NPS.Math;
using NPS.Loot;
using UnityEngine;

public class UIItemCurrency : UIItem
{
    [SerializeField] private string rsIcon = "Currency";

    public override void Set(object data)
    {
        var item = data as CurrencyData;

        imgIcon.sprite = ResourceManager.S.LoadSprite(rsIcon, item.Type.ToString());
        txtAmount.text = $"x{item.Value.Show()}";
    }
}
