using System.Collections;
using UnityEngine;
using BayatGames.SaveGameFree;
using UnityEngine.SceneManagement;
using System.Text;
using BayatGames.SaveGameFree.Encoders;
using BayatGames.SaveGameFree.Serializers;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using MEC;
using NPS;
using System;
using Hellmade.Net;

public class SplashGame : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI txtVersion;
    [SerializeField] private float time = 3f;
    [SerializeField] private GameObject btnTap2Play;
    [SerializeField] private Image loadingImg;
    [SerializeField] private GameObject loadingBar;

    private void Awake()
    {
        txtVersion.text = "Version: " + Application.version;
    }

    private void Start()
    {
        AppManager.S.Init();
        DataManager.S.Init();

        Timing.RunCoroutine(_Loading());

        AppManager.Ads?.HideBanner();
    }  

    private void ChangeScene()
    {
        MonoScene.S.LoadAsync("Main");
    }

    private IEnumerator<float> _Loading()
    {
        float t = 0;

        while (true)
        {
            t += Timing.DeltaTime;
            loadingImg.fillAmount = t / time;
            if (t >= time) break;

            yield return Timing.DeltaTime;
        }

        loadingImg.fillAmount = 1f;
        btnTap2Play.SetActive(true);
        loadingBar.SetActive(false);

        ChangeScene();

        EazyNetChecker.OnConnectionStatusChanged += OnNetStatusChanged;
        EazyNetChecker.StartConnectionCheck(false, true);

        yield break;
    }

    private void OnNetStatusChanged()
    {
        if (EazyNetChecker.Status == NetStatus.Connected)
        {
            EazyNetChecker.OnConnectionStatusChanged -= OnNetStatusChanged;
            ChangeScene();
        }
        else if (EazyNetChecker.Status == NetStatus.NoDNSConnection || EazyNetChecker.Status == NetStatus.WalledGarden)
        {
            EazyNetChecker.StartConnectionCheck(false, true);
        }
    }

    public void Play()
    {
        MonoScene.S.Active();
    }
}
