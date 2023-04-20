using System;
using System.Collections;
using System.Collections.Generic;
using com.unimob.mec;
using NPS.Pattern.Observer;
using UnityEngine;
using UnityEngine.UI;

public class UIButtonCurrency : MonoBehaviour
{
    [SerializeField] private CurrencyType type = CurrencyType.Diamond;
    [SerializeField] private UICurrency uiCurrency;
    [SerializeField] private Image imgContent;
    [SerializeField] private Sprite spActive;
    [SerializeField] private Sprite spInActive;

    private Action success;
    private Action failure;
    private Func<bool> condition;
    private bool active = false;

    private void OnEnable()
    {
        this.RegisterListener(EventID.ChangeCurrency, OnChangeCurrency);
    }

    private void OnChangeCurrency(object obj)
    {
        CurrencyType type = (CurrencyType)obj;
        if (this.type == type)
        {
            UpdateUI();
        }
    }

    public void UpdateUI()
    {
        if (condition != null)
        {
            active = condition.Invoke();
            Active(active);
        }
    }

    private void OnDisable()
    {
        this.RemoveListener(EventID.ChangeCurrency, OnChangeCurrency);
    }

    public void Set(Action success, Func<bool> condition, Action failure = null)
    {
        this.success = success;
        this.failure = failure;
        this.condition = condition;
    }

    public void Set(CurrencyData currency)
    {
        Set(currency.Type);
        Set(currency.Value);
    }

    public void Set(CurrencyType type)
    {
        this.type = type;
        uiCurrency.Set(type);
    }

    public void Set(double currency)
    {
        uiCurrency.Set(currency);
    }

    public void Set(string content)
    {
        uiCurrency.Set(content);
    }

    public void Active(bool active)
    {
        imgContent.sprite = active ? spActive : spInActive;
        uiCurrency.Active(active);
    }

    public void OnClick()
    {
        if (active)
        {
            success?.Invoke();
        }
        else
        {
            failure?.Invoke();
            MainGameScene.S.Toast.Show(
                I2.Loc.LocalizationManager.GetTranslation(
                    $"You don't have enough {Utils.GetCurrency(type).ToLower()}"));
        }
    }
}