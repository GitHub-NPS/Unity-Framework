using NPS.Pattern.Observer;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NPS.Loot
{
    public class UILoot : MonoBehaviour
    {
        [SerializeField] private GameObject content;

        [SerializeField] private Transform lootContent;
        [SerializeField] private List<ItemData> lstLootItem;

        private Dictionary<LootType, ItemData> dicLootItem = new Dictionary<LootType, ItemData>();
        private List<UIItem> items = new List<UIItem>();

        private void Awake()
        {
            foreach (var item in lstLootItem)
            {
                if (!dicLootItem.ContainsKey(item.Type)) dicLootItem.Add(item.Type, item);
            }
        }

        public void Show(List<LootData> rewards)
        {
            foreach (var item in rewards)
            {
                if (dicLootItem.ContainsKey(item.Type))
                {
                    var obj = Pooling.Manager.S.Spawn(dicLootItem[item.Type].Item, lootContent);
                    obj.Set(item.Data);
                    obj.Light(true);

                    items.Add(obj);
                }
            }

            content.SetActive(true);
        }

        public void Continue()
        {
            foreach (var item in items)
            {
                Pooling.Manager.S.Despawn(item.gameObject);
            }
            items.Clear();

            content.SetActive(false);
        }

        public void Loot(List<LootData> rewards)
        {
            var contain = new List<LootType>();

            var loots = rewards.Merge();
            for (int i = 0; i < loots.Count; i++)
            {
                Loot(loots[i]);

                if (!contain.Contains(loots[i].Type))
                    contain.Add(loots[i].Type);
            }

            // Save

            foreach (var item in contain)
                this.PostEvent(EventID.LootReward, item);
        }

        public void Loot(LootData reward)
        {
            switch (reward.Type)
            {
                case LootType.Currency:
                    //MainGameScene.S.Fix.Loot(reward.Data as CurrencyData, true, true);
                    break;
            }
        }
    }
}