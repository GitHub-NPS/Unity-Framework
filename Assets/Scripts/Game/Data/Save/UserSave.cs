using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using UnityEngine;
using NPS.Pattern.Observer;
using BayatGames.SaveGameFree;

[System.Serializable]
public class UserSave : IDataSave
{
    public bool Logged => !string.IsNullOrEmpty(googleId) || !string.IsNullOrEmpty(appleId) || !string.IsNullOrEmpty(facebookId);
    public string Key => key;
    public string key;

    public UserSave(string key)
    {
        this.key = key;

        foreach (CurrencyType currency in (CurrencyType[])Enum.GetValues(typeof(CurrencyType)))
        {
            Currency.Add(currency, 0);
        }

        Currency[CurrencyType.Coin] = -1;
    }

    [Button]
    public void Save()
    {
        SaveGame.Save(Key, this);
    }

    [ShowInInspector] public Dictionary<CurrencyType, double> Currency = new Dictionary<CurrencyType, double>();

    public string uId;
    public string devicedId;

    public string googleId;
    public string appleId;
    public string facebookId;

    public string name = "You";
    public string avatar = "";

    public int CloudVersion = 0;

    public int rank = -1;
    public float complete = -1f;

    public List<int> Point = new List<int>();
    public bool FirtsMap = false;

    public void Clear()
    {
        Currency[CurrencyType.Coin] = -1;
    }

    public void Fix()
    {
        if (Currency == null) Currency = new Dictionary<CurrencyType, double>();

        if (Point == null) Point = new List<int>();

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

    public void SetRank(int value)
    {
        rank = value;
        Save();
    }

    public void ResetRank()
    {
        rank = -1;
        complete = 0;
      Save();
    }

    public void SetComplete(float value)
    {
        if (value != 0) complete = value;
        Save();
    }
}
