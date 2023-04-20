using BansheeGz.BGDatabase;
using System;

[System.Serializable]
public class GemEntity
{
    public int Quantity;
    public string Product;

    public GemEntity()
    {

    }

    public GemEntity(BGEntity entity)
    {
        Quantity = entity.Get<int>("Quantity");
        Product = entity.Get<string>("Product");
    }
}
