using NPS.Math;
using com.unimob.timer;
using TMPro;
using UnityEngine;

public abstract class UIEvent: UIView, IPopup
{
    [SerializeField] private TextMeshProUGUI txtTime;

    private TickData tick = new TickData(TimerType.RealtimeUpdate, 1);

    public override void Show(object obj = null)
    {
        base.Show(obj);

        tick.Action = Tick;
        tick.RegisterTick();
    }

    private void Tick()
    {
        var time = DataManager.Save.RemoteConfig.EventTime;
        if (time.TotalSeconds <= 0)
            tick.RemoveTick();

        txtTime.text = string.Format(I2.Loc.LocalizationManager.GetTranslation("Event end in:"), time.Show());
    }

    public override void Hide()
    {
        tick.RemoveTick();

        base.Hide();
    }
}