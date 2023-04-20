using com.unimob.mec;
using NPS.Pattern.Observer;
using NPS.Math;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIEventNoel : UIEvent
{
    [SerializeField] private TextMeshProUGUI txtSock;
    [SerializeField] private Transform container;
    [SerializeField] private ContentSizeFitter fitter;
    [SerializeField] private ScrollRect sc;

    private UserSave userSave;

#if UNITY_EDITOR
    protected override void OnValidate()
    {
        base.OnValidate();

        fitter = container.GetComponent<ContentSizeFitter>();
    }
#endif

    protected override void Init()
    {
        base.Init();

        userSave = DataManager.Save.User;
    }

    public override void Show(object obj = null)
    {
        base.Show(obj);

        Timing.RunCoroutine(_Resize());
        txtSock.text = userSave.Currency[CurrencyType.Point].Show();

        this.RegisterListener(EventID.ChangeCurrency, OnChangeCurrency);
    }

    private void OnChangeCurrency(object obj)
    {
        CurrencyType type = (CurrencyType)obj;
        if (type == CurrencyType.Point)
        {
            txtSock.text = userSave.Currency[CurrencyType.Point].Show();
        }
    }

    private IEnumerator<float> _Resize(bool scroll = true)
    {
        fitter.enabled = false;
        yield return Timing.WaitForOneFrame;
        fitter.enabled = true;

        if (scroll) sc.verticalNormalizedPosition = 1;
    }

    public override void Hide()
    {
        this.RemoveListener(EventID.ChangeCurrency, OnChangeCurrency);

        base.Hide();
    }

    private void OnDestroy()
    {
        this.RemoveListener(EventID.ChangeCurrency, OnChangeCurrency);
    }

    public void Infor()
    {
#if DEVELOPMENT || UNITY_EDITOR || STAGING
        DataManager.Save.User.IncreaseCurrency(new CurrencyData()
        {
            Type = CurrencyType.Point,
            Value = 100
        });
#endif

        MainGameScene.S.Show<UIInforEventNoel>();
    }

    public void BuySuccess()
    {
        Timing.RunCoroutine(_Resize(false));
    }
}
