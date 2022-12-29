using com.unimob.timer;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoRotateObject : MonoBehaviour
{
    [SerializeField] private bool enable = false;
    [SerializeField] private float speed = 10f;

    private TickData tick = new TickData();
    private Quaternion origin;

    private void Awake()
    {
        origin = this.transform.localRotation;
    }

    private void OnEnable()
    {
        if (enable) AutoRotate();
    }

    public void Set(float speed)
    {
        this.speed = speed;
    }

    public void AutoRotate()
    {
        tick.Action = Tick;
        tick.RegisterTick();
    }

    private void OnDisable()
    {
        tick.RemoveTick();
        this.transform.localRotation = origin;
    }

    private void Tick()
    {
        float v = speed * Time.deltaTime * -200;
        this.transform.Rotate(new Vector3() { z = v });
    }
}
