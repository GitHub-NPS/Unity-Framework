using NPS.Math;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UICurrency : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI txtContent;
    [SerializeField] private Image imgIcon;

    [SerializeField] private Color32 clActive;
    [SerializeField] private Color32 clInActive;

    private CurrencyType type = CurrencyType.Coin;

    public void Set(CurrencyType type)
    {
        if (type == this.type) return;

        this.type = type;
        imgIcon.sprite = ResourceManager.S.LoadSprite("Currency", Utils.GetCurrency(type));
    }

    public void SetIcon(string content)
    {
        imgIcon.sprite = ResourceManager.S.LoadSprite("Currency", content);
    }
    
    public void Set(double currency)
    {
        txtContent.text = currency.Show();
    }

    public void Active(bool active)
    {
        txtContent.color = active ? clActive : clInActive;
    }

    public void Set(string text)
    {
        txtContent.text = text;
    }
}