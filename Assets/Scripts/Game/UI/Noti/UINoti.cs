using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class UINoti : MonoBehaviour
{
    public UnityEvent<bool> OnChange;
    public bool IsNoti = false;
    public bool IsEnable = true;

    [SerializeField] private GameObject content;

#if UNITY_EDITOR
    [Obsolete]
    private void OnValidate()
    {
        var find = this.transform.Find("Content");
        if (find) content = find.gameObject;
    }
#endif

    private void OnEnable()
    {
        if (IsEnable)
            UpdateNoti();
    }

    public void UpdateNoti()
    {
        IsNoti = HasNoti();
        iUpdate(IsNoti);
    }

    protected void iUpdate(bool isNoti)
    {
        OnChange?.Invoke(isNoti);
        if (content) content.SetActive(IsNoti);
    }

    protected abstract bool HasNoti();
}
