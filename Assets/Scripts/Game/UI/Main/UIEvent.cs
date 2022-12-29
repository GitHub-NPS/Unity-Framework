using NPS.Math;
using com.unimob.timer;
using TMPro;
using UnityEngine;

public abstract class UIEvent : UIView
{
    [SerializeField] private TextMeshProUGUI txtTime;

    protected EventData data;
    private TickData tick = new TickData(TimerType.RealtimeUpdate, 1);

    protected override void Init()
    {
        data = DataManager.Save.RemoteConfig.Event;

        base.Init();
    }

    public override void Show(object obj = null)
    {
        base.Show(obj);

        tick.Action = Tick;
        tick.RegisterTick();
    }

    private void Tick()
    {
        if (data.Time.TotalSeconds <= 0)
            tick.RemoveTick();

        txtTime.text = string.Format(I2.Loc.LocalizationManager.GetTranslation("Event end in:"), data.Time.Show());
    }

    public override void Hide()
    {
        tick.RemoveTick();

        base.Hide();
    }
}
