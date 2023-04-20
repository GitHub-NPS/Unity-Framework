using System.Collections;
using UnityEngine;
using System;

public class SplashGameManager : MonoBehaviour
{
    [SerializeField] private UILoading Loading;

    private void Start()
    {
        Loading.Show();
        Loading.Loading(2f, () =>
        {
            Loading.Tap2Continue(() =>
            {
                var userSave = DataManager.Save.User;
                MonoScene.S.Load("Main");
            });
        }, false);

        AppManager.Ads?.HideBanner();
    }
}
