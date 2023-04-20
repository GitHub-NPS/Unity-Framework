using System;
using System.Collections.Generic;
using com.unimob.mec;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UILoading : UIView
{
    [SerializeField] private TextMeshProUGUI txtVersion;
    [SerializeField] private GameObject btnTap2Play;
    [SerializeField] private Image loadingImg;
    [SerializeField] private GameObject loadingBar;
    [SerializeField] private TextMeshProUGUI txtLoading;

    private CoroutineHandle handle;
    private float time = 1.5f;
    private Action callback;
    private Action tap2Continue;

    private float t = 0;

    protected override void Init()
    {
        base.Init();

        txtVersion.text = "Version: " + Application.version;
    }

    public override void Show(object obj = null)
    {
        Initialize();

        content.SetActive(true);
    }

    public override void Hide()
    {
        content.SetActive(false);
    }

    public void Loading(float time, Action callback = null, bool reset = true)
    {
        this.time = time;
        this.callback = callback;

        if (reset) Reset();

        if (handle.IsValid) Timing.KillCoroutines(handle);
        handle = Timing.RunCoroutine(_Loading(), Segment.RealtimeUpdate);
    }

    private void Reset()
    {
        t = 0;
        loadingImg.fillAmount = 0f;

        btnTap2Play.SetActive(false);
        loadingBar.SetActive(true);
    }

    private IEnumerator<float> _Loading()
    {
        while (true)
        {
            t += Timing.DeltaTime;
            loadingImg.fillAmount = t / time;
            if (t >= time) break;

            yield return Timing.WaitForOneFrame;
        }

        loadingImg.fillAmount = 1f;
        callback?.Invoke();

        yield break;
    }

    public void Tap2Continue()
    {
        tap2Continue?.Invoke();
    }

    public void Tap2Continue(Action callback)
    {
        this.tap2Continue = callback;

        btnTap2Play.SetActive(true);
        loadingBar.SetActive(false);
        txtLoading.gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        if (handle.IsValid) Timing.KillCoroutines(handle);
    }
}
