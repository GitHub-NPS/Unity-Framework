using NPS.Math;
using com.unimob.timer;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public abstract class UIIconEventItem : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI txtTime;

    private TickData tick = new TickData(TimerType.RealtimeUpdate, 1);

    public virtual void Set()
    {
        tick.Action = Tick;
        tick.RegisterTick();
    }

    private void Tick()
    {
        var time = DataManager.Save.RemoteConfig.EventTime;

        if (time.TotalSeconds <= 0)
        {
            tick.RemoveTick();
            this.gameObject.SetActive(false);
        }

        txtTime.text = time.Show();
    }

    public abstract void OnClick();
}