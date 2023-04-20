using com.unimob.timer;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchDetection : MonoBehaviour
{
    [SerializeField] private GameObject effect;

    private TickData tick = new TickData();

    private void Start()
    {
        tick.Action = Tick;
        tick.RegisterTick();
    }

    private void Tick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var eff = NPS.Pooling.Manager.S.Spawn(effect, this.transform);
            var pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            pos.z = 0;
            eff.transform.position = pos;
        }
    }

    private void OnDestroy()
    {
        tick.RemoveTick();
    }
}
