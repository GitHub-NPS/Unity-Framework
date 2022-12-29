using System.Collections;
using UnityEngine;
using com.unimob.mec;
using NPS.Pattern.Observer;

public class SplashGameManager : MonoBehaviour
{
    [SerializeField] UILoading Loading;

    private void Awake()
    {
        this.RegisterListener(EventID.LoadSuccess, OnLoadSuccess);
    }

    private void OnLoadSuccess(object obj)
    {
        ChangeScene();
    }

    private void OnDestroy()
    {
        Timing.KillCoroutines();

        this.RemoveListener(EventID.LoadSuccess, OnLoadSuccess);
    }

    private void Start()
    {
        DataManager.S.Init();
        AppManager.S.Init();

        Loading.Show();
        Loading.Loading(3f, () =>
        {
            Loading.Tap2Continue(() =>
            {
                MonoScene.S.Active();
            });
        }, false);

        AppManager.Ads?.HideBanner();
    }

    private void ChangeScene()
    {
        MonoScene.S.LoadAsync("Main");
    }
}
