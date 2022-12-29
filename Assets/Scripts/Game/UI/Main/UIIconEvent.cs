using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIIconEvent : MonoBehaviour
{
    public Transform TranIcon => this.gameObject.transform;
    private EventData data;

    private void Awake()
    {
        data = DataManager.Save.RemoteConfig.Event;
    }

    private void Start()
    {
        if (data.IsValid)
        {
            var ui = Instantiate(ResourceManager.S.LoadEvent($"UIIconEvent{data.Type}"), this.transform);
            ui.Set();
        }
    }
}
