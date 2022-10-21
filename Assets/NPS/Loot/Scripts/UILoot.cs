using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

namespace NPS
{
    namespace Loot
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
                        var obj = PoolManager.S.Spawn(dicLootItem[item.Type].Item, lootContent);
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
                    PoolManager.S.Despawn(item.gameObject);
                }
                items.Clear();

                content.SetActive(false);
            }
        }
    }
}