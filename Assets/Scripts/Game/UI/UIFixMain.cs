using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIFixMain : MonoBehaviour
{
    [SerializeField] private GameObject content;
    [SerializeField] private List<UIFixItem> items;

    [SerializeField] private UIDummy dummyUi;

    private UserSave userSave;
    private Dictionary<CurrencyType, UIFixItem> dicItem = new Dictionary<CurrencyType, UIFixItem>();

    private void Awake()
    {
        userSave = DataManager.Save.User;
    }

    private void Init()
    {
        if (dicItem.Count == 0)
        {
            foreach (var item in items)
            {
                dicItem.Add(item.Type, item);
            }
        }
    }

    public void Loot(CurrencyData data, Transform start = null, Transform end = null, Action complete = null,
        float delay = 0.4f)
    {
        Init();

        var tran = end ? end : dicItem[data.Type].TranIcon;

        var size = Math.Min(data.Value, 10);
        var incre = data.Value / size;
        var count = 0;
        for (var i = 0; i < size; i++)
        {
            var co = count != size - 1 ? incre : (incre + data.Value - incre * size);

            var dummy = NPS.Pooling.Manager.S.Spawn(dummyUi, tran);

            dummy.Set(Utils.GetCurrency(data.Type));
            dummy.Loot(start ? start : this.transform, tran, () =>
            {
                userSave.IncreaseCurrency(data.Type, co);
                count++;
                if (count == size)
                {
                    userSave.Save();
                    complete?.Invoke();
                }
            }, delay);
        }
    }

    public void Loot(double value, string content, Transform start = null, Transform end = null, Action complete = null,
        float delay = 0.4f)
    {
        var tran = end;
        var size = Math.Min(value, 10);
        var incre = value / size;
        var count = 0;
        for (var i = 0; i < size; i++)
        {
            var co = count != size - 1 ? incre : (incre + value - incre * size);

            var dummy = NPS.Pooling.Manager.S.Spawn(dummyUi, tran);
            dummy.Set(content);
            dummy.Loot(start ? start : this.transform, tran, () =>
            {
                count++;
                if (count == size)
                    complete?.Invoke();
            }, delay);
        }
    }
}