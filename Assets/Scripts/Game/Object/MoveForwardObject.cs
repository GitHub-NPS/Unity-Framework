using com.unimob.timer;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveForwardObject : MonoBehaviour
{
    [SerializeField] private bool enable = false;
    [SerializeField] private float speed = 10f;
    [SerializeField] private float angle = 0;

    private TickData tick = new TickData();

    private void OnEnable()
    {
        if (enable) Move();
    }

    public void Set(float angle, float speed)
    {
        this.angle = angle;
        this.speed = speed;

        this.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    public void Set(float speed)
    {
        this.speed = speed;
    }

    public void Move()
    {
        tick.Action = Tick;
        tick.RegisterTick();
    }

    private void Tick()
    {
        transform.Translate(Vector3.right * speed * Time.deltaTime);
    }

    public void Stop()
    {
        tick.RemoveTick();
    }

    private void OnDisable()
    {
        tick.RemoveTick();
    }
}
