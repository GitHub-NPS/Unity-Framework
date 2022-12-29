using System;
using NPS.Loot;

[System.Serializable]
public class CurrencyData: ILootData
{
    public CurrencyType Type = CurrencyType.Coin;
    public double Value = 0;    

    public CurrencyData()
    {

    }

    public CurrencyData(string content)
    {
        string[] str = content.Split(';');
        Enum.TryParse(str[1], out CurrencyType currencyType);
        Type = currencyType;
        Value = double.Parse(str[2]);
    }

    public ILootData Clone()
    {
        CurrencyData clone = new CurrencyData();
        clone.Type = Type;
        clone.Value = Value;

        return clone;
    }

    public bool Same(ILootData data)
    {
        return Type == (data as CurrencyData).Type;
    }

    public void Add(ILootData data)
    {
        Value += (data as CurrencyData).Value;
    }
}
