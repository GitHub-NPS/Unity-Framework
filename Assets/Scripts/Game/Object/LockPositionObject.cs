using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using com.unimob.timer;

public class LockPositionObject : MonoBehaviour
{
    [SerializeField] private bool enable = false;

    private TickData tick = new TickData();

    private void OnEnable()
    {
        if (enable) LockPosition();
    }

    public void LockPosition()
    {
        tick.Action = Tick;
        tick.RegisterTick();
    }

    private void OnDisable()
    {
        tick.RemoveTick();
    }

    private void Tick()
    {
        this.transform.localPosition = Vector3.zero;
    }
}
