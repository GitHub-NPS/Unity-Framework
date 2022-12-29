using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using UnityEngine;
using NPS.Pattern.Observer;

[System.Serializable]
public class UserSave : ADataSave
{
    [ShowInInspector] public Dictionary<CurrencyType, double> Currency = new Dictionary<CurrencyType, double>();

    public string id = "";
    public string name = "You";
    public string avatar = "";
    public int rank = -1;

    public List<int> Noel = new List<int>();

    public void Clear()
    {
        Currency[CurrencyType.Coin] = -1;
    }

    public UserSave(string key) : base(key)
    {
        foreach (CurrencyType currency in (CurrencyType[])Enum.GetValues(typeof(CurrencyType)))
        {
            Currency.Add(currency, 0);
        }

        Currency[CurrencyType.Coin] = -1;
        Currency[CurrencyType.Diamond] = 0;
    }

    public override void Fix()
    {
        base.Fix();

        foreach (CurrencyType currency in (CurrencyType[])Enum.GetValues(typeof(CurrencyType)))
        {
            if (!Currency.ContainsKey(currency))
                Currency.Add(currency, 0);
        }
    }

    public void SetCurrency(CurrencyData currency)
    {
        SetCurrency(currency.Type, currency.Value);
    }

    public void SetCurrency(CurrencyType type, double value)
    {
        value = value >= 1000 ? value : Mathf.CeilToInt((float)value);
        value = value < 0 ? 0 : value;
        Currency[type] = value;

        Observer.S?.PostEvent(EventID.ChangeCurrency, type);
    }

    public void IncreaseCurrency(CurrencyData currency)
    {
        IncreaseCurrency(currency.Type, currency.Value);
    }

    public void IncreaseCurrency(CurrencyType type, double value)
    {
        Currency[type] += value;

        Observer.S?.PostEvent(EventID.ChangeCurrency, type);
    }

    public void DecreaseCurrency(CurrencyData currency)
    {
        DecreaseCurrency(currency.Type, currency.Value);
    }

    public void DecreaseCurrency(CurrencyType type, double value)
    {
        value = value >= 1000 ? value : Mathf.CeilToInt((float)value);

        Currency[type] = Currency[type] >= value ? Currency[type] - value : 0;

        Observer.S?.PostEvent(EventID.ChangeCurrency, type);
    }
}
