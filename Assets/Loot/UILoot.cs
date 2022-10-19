using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class UILoot : MonoBehaviour
{
    public static UILoot S;

    [SerializeField] private GameObject content;

    [SerializeField] private Transform lootContent;
    [SerializeField] private List<LootItem> lstLootItem;
    private Dictionary<LootType, LootItem> dicLootItem = new Dictionary<LootType, LootItem>();
    private List<UILootItem> items = new List<UILootItem>();

    private void Awake()
    {
        if (!S) S = this;

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
