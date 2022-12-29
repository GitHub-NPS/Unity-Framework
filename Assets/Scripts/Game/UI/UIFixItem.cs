using TMPro;
using UnityEngine;
using UnityEngine.UI;
using NPS.Math;
using NPS.Pattern.Observer;
using System;

public abstract class UIFixItem : MonoBehaviour
{
    public CurrencyType Type => type;
    public Transform TranIcon => imgIcon.gameObject.transform;

    [SerializeField] protected CurrencyType type;
    [SerializeField] private TextMeshProUGUI txtAmount;
    [SerializeField] protected Image imgIcon;

    private UserSave userSave;

    protected virtual void Awake()
    {
        userSave = DataManager.Save.User;

        this.RegisterListener(EventID.ChangeCurrency, OnChangeCurrency);
    }

    protected virtual void Start()
    {
        UpdateCurrency(type);
    }

    private void OnChangeCurrency(object obj)
    {
        UpdateCurrency((CurrencyType)obj);
    }

    private void OnDestroy()
    {
        this.RemoveListener(EventID.ChangeCurrency, OnChangeCurrency);
    }

    private void Set(double amount)
    {
        txtAmount.text = amount.Show();
    }

    public abstract void OnClick();

    private void UpdateCurrency(CurrencyType type)
    {
        if (userSave != null  && this.type == type)
        {
            Set(userSave.Currency[type]);
        }
    }
}
