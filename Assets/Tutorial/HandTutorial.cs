using System;
using MEC;
using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NPS;

public abstract class HandTutorial : MonoBehaviour
{
    [SerializeField] protected GameObject hand;
    [SerializeField] private float speed = 2;
    [SerializeField] private GameObject fx;

    public abstract void Set(HandType type);

    public IEnumerator<float> _Move(Transform end, Transform start, bool isLoop = false)
    {
        fx.SetActive(true);

        while (true)
        {
            float step = speed * Time.fixedDeltaTime;
            this.gameObject.transform.position = Vector3.MoveTowards(transform.position, end.position, step);

            if (MathHelper.SqrMagnitude(transform.position, end.position) < 0.001f)
            {
                if (isLoop)
                {
                    fx.SetActive(false);
                    this.transform.position = start.position;
                    fx.SetActive(true);
                }
                else break;
            }

            yield return Timing.WaitForOneFrame;
        }
    }
}