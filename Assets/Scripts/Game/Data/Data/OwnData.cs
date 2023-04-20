using System;

[System.Serializable]
public class OwnData
{
    public OwnType Type;
    public object Data;

    public OwnData()
    {

    }

    public OwnData(string content)
    {
        if (string.IsNullOrEmpty(content)) return;

        string[] str = content.Split(';');
        Enum.TryParse(str[0], out OwnType type);
        Type = type;
        switch (Type)
        {
            case OwnType.Currency:
                Data = new CurrencyData(content);
                break;
            case OwnType.Ads:
                if (str.Length > 1) Data = int.Parse(str[1]);
                break;
        }
    }
}
