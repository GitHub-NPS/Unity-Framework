using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using com.unimob.pattern.singleton;

namespace NPS.Loot
{
    public class Manager : MonoSingleton<LootManager>
    {
        protected static UILoot ui;
        public static UILoot UI
        {
            get
            {
                if (!ui) ui = FindObjectOfType<UILoot>();
                return ui;
            }
        }

        public void Loot(List<LootData> loots, bool isUI = false)
        {
            foreach (var item in loots)
            {
                Loot(item);
            }

            DataManager.Save.User.Save();

            if (isUI) UI.Show(loots);
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
}
