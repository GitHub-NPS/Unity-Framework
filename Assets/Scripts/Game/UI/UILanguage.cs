using System.Collections;
using System.Collections.Generic;
using I2.Loc;
using UnityEngine;
using System;
using UnityEngine.UI;

public class UILanguage: UIView, IPopup
{
    [SerializeField] private Transform itemRoot;
    [SerializeField] private UILanguageItem languageItem;

    private List<UILanguageItem> items = new List<UILanguageItem>();

    public override void Show(object obj = null)
    {
        base.Show(obj);
        var languages = LocalizationManager.GetAllLanguages();
        
        items.Clear();
        foreach (var item in languages)
        {
            var languageItem = NPS.Pooling.Manager.S.Spawn(this.languageItem, itemRoot);
            languageItem.SetView(item);
            languageItem.gameObject.SetActive(true);
            
            items.Add(languageItem);
        }
    }
    
    public override void Hide()
    {
        base.Hide();

        foreach (var item in items)
        {
            NPS.Pooling.Manager.S.Despawn(item.gameObject);
        }
        items.Clear();
    }
}
