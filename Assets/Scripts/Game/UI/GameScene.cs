using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScene<T> : MonoBehaviour
{
    public static T S;

    [SerializeField] private Transform posPopup;

#if UNITY_EDITOR
    private void OnValidate()
    {
        posPopup = this.transform.Find("SafeArea/Popup");
    }
#endif

    protected virtual void Awake()
    {
        S = this.GetComponent<T>();
    }

    protected virtual void Start()
    {

    }

    protected virtual void OnDestroy()
    {

    }

    protected Dictionary<Type, UIView> views = new Dictionary<Type, UIView>();

    public virtual void Show<V>(object obj = null) where V : UIView
    {
        if (!views.ContainsKey(typeof(V)))
        {
            views.Add(typeof(V), Instantiate(ResourceManager.S.LoadView(typeof(V).ToString()), posPopup));
        }
        views[typeof(V)].gameObject.transform.SetAsLastSibling();
        views[typeof(V)].Show(obj);
    }

    public V View<V>() where V : UIView
    {
        if (!views.ContainsKey(typeof(V)))
            Show<V>();

        return views[typeof(V)] as V;
    }

    public virtual void Hide<V>() where V : UIView
    {
        if (views.ContainsKey(typeof(V)))
        {
            views[typeof(V)].Hide();
        }
    }

    public virtual void Show(UIView view)
    {
        view.content.SetActive(true);
    }

    public virtual void Hide(UIView view)
    {
        view.content.SetActive(false);
    }
}
