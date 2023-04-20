using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIToast : MonoBehaviour
{
    [SerializeField] private UIToastItem itemPrb;

    public void Show(string content)
    {
        var item = NPS.Pooling.Manager.S.Spawn(itemPrb, this.transform);
        item.Set(content);
        item.transform.localPosition = Vector3.zero;
    }
}
