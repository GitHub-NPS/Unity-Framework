using System.Collections;
using System.Collections.Generic;
using NPS;
using UnityEngine;

public abstract class UIView : MonoBehaviour
{
    [SerializeField] protected GameObject content;

    private bool isInit = false;

#if UNITY_EDITOR
    private void OnValidate()
    {
        if (content == null)
            content = this.transform.Find("Content") ? this.transform.Find("Content").gameObject : this.gameObject;
    }
#endif

    private void Initialize()
    {
        if (isInit) return;
        Init();
    }

    protected virtual void Init()
    {
        isInit = true;
    }

    public virtual void Show(object obj = null)
    {
        Initialize();

        content.SetActive(true);
    }

    public virtual void Hide()
    {
        content.SetActive(false);
    }
}
