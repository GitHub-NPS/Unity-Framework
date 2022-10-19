using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NPS;

#if HAS_LION_GAME_ANALYTICS_SDK
using GameAnalyticsSDK;
#endif

public class GameManager : MonoBehaviour
{
    public static GameManager S;

    private void Awake()
    {
        if (!S) S = this;

        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        Application.targetFrameRate = 120;
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");

#if HAS_LION_GAME_ANALYTICS_SDK
        GameAnalytics.Initialize();
#endif
        TrackingEvent.StartInit();
        DontDestroyOnLoad(this);

#if DEVELOPMENT
        if (Application.isEditor)
            Application.runInBackground = true;
#endif
    }

    private void Start()
    {

    }
}
