using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NPS;

public class LootManager : MonoSingleton<LootManager>
{
    public void Loot(List<LootData> loots, bool isUI = false)
    {
        foreach (var item in loots)
        {
            Loot(item);
        }

        DataManager.Save.User.Save();

        if (isUI) UILoot.S.Show(loots);
    }

    public void Loot(LootData loot)
    {
        switch (loot.Type)
        {
            case LootType.Currency:
                var currency = loot.Data as CurrencyData;
                DataManager.Save.User.IncreaseCurrency(currency);
                break;
        }
    }
}
