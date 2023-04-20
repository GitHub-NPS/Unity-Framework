using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UINotiLink : MonoBehaviour
{
    [SerializeField] private GameObject content;

    [SerializeField] private List<UINoti> childs;

    private bool isNoti = false;

#if UNITY_EDITOR
    private void OnValidate()
    {
        content = this.transform.Find("Content").gameObject;
        foreach (var child in childs)
        {
            child.IsEnable = false;
        }
    }
#endif

    private void Awake()
    {
        foreach (var child in childs)
        {
            child.OnChange.AddListener(UpdateNoti);
        }
    }

    private void OnEnable()
    {
        foreach (var child in childs)
        {
            child.UpdateNoti();
        }
    }

    private void OnDestroy()
    {
        foreach (var child in childs)
        {
            child.OnChange.RemoveListener(UpdateNoti);
        }
    }

    private void UpdateNoti(bool isNoti)
    {
        if (this.isNoti == isNoti) return;

        if (!isNoti)
        {
            foreach (var child in childs)
            {
                if (child.IsNoti) return;
            }
        }

        this.isNoti = isNoti;
        content?.SetActive(isNoti);
    }
}
