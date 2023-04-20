using NPS.Pattern.Observer;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIIconEvent : MonoBehaviour
{
    public Transform TranIcon => this.gameObject.transform;

    private RemoteConfigSave remote;

    private void Awake()
    {
        remote = DataManager.Save.RemoteConfig;

        this.RegisterListener(EventID.StartGameSuccess, StartGameSuccess);
    }

    private void StartGameSuccess(object obj)
    {
        TutorialManager.S.RegisterComplete(101, () =>
        {
            remote.CheckEvent();
            Init();
        });

        Init();
    }

    private void OnDestroy()
    {
        this.RemoveListener(EventID.StartGameSuccess, StartGameSuccess);
    }

    private void Init()
    {
        if (remote.EnableEvent)
        {
            var ui = Instantiate(ResourceManager.S.LoadEvent($"UIIconEvent{DataManager.Save.RemoteConfig.Event.Type}"),
                this.transform);
            ui.Set();
        }
    }
}