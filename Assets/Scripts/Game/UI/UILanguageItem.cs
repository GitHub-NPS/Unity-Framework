using System;
using System.Collections;
using System.Collections.Generic;
using I2.Loc;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class UILanguageItem : MonoBehaviour
{
    [SerializeField] private GameObject mask;
    [SerializeField] private TextMeshProUGUI tmpLanguage;

    private string _language;

    public void SetView(string _language)
    {
        this._language = _language;
        mask.SetActive(LocalizationManager.CurrentLanguage.Equals(_language));
        tmpLanguage.text = DB_Language.GetEntity(_language) == null
            ? $"{_language}"
            : DB_Language.GetEntity(_language).Language;
    }

    public void OnClick_ChangeLanguage()
    {
        var code = LocalizationManager.GetLanguageCode(_language);
        LocalizationManager.SetLanguageAndCode(_language, code, true, true);
        MainGameScene.S.Hide<UILanguage>();
    }
}