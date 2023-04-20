using System.Collections;
using System.Collections.Generic;
using NPS;
using UnityEngine;

public abstract class UIView : MonoBehaviour
{
    [Header("View")]
    public bool IsBack = false;

    [SerializeField] public GameObject content;

    private bool isInit = false;

#if UNITY_EDITOR
    protected virtual void OnValidate()
    {
        if (content == null)
            content = this.transform.Find("Content") ? this.transform.Find("Content").gameObject : this.gameObject;
    }
#endif

    protected void Initialize()
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
        MainGameScene.S.Show(this);

        Initialize();
    }

    public virtual void Hide()
    {
        MainGameScene.S.Hide(this);
    }

    public virtual void Back()
    {
        Hide();
    }
}
