using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using com.unimob.timer;

public class AutoFollowTarget : MonoBehaviour
{
    private TickData tick = new TickData(TimerType.SlowUpdate);

    private void OnDisable()
    {
        UnFollow();
    }

    public void Follow(Transform target)
    {
        UnFollow();

        tick.Action = () => Tick(target);
        tick.RegisterTick();
    }

    private void Tick(Transform target)
    {
        if (!target || !target.gameObject.activeSelf)
            UnFollow();

        this.transform.position = target.position;
    }

    public void UnFollow()
    {
        tick.RemoveTick();
    }
}
