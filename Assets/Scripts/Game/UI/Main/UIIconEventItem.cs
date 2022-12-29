using NPS.Math;
using com.unimob.timer;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public abstract class UIIconEventItem : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI txtTime;

    protected EventData data;
    private TickData tick = new TickData(TimerType.RealtimeUpdate, 1);

    private void Awake()
    {
        data = DataManager.Save.RemoteConfig.Event;
    }

    public virtual void Set()
    {
        tick.Action = Tick;
        tick.RegisterTick();
    }

    private void Tick()
    {
        if (data.Time.TotalSeconds <= 0)
        {
            tick.RemoveTick();
            this.gameObject.SetActive(false);
        }

        txtTime.text = data.Time.Show();
    }

    public abstract void OnClick();
}
