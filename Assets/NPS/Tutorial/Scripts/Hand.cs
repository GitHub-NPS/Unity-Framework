using System.Collections.Generic;
using UnityEngine;
using NPS.Math;
using com.unimob.timer;

namespace NPS.Tutorial
{
    public abstract class Hand : MonoBehaviour
    {
        [SerializeField] protected GameObject hand;
        [SerializeField] private float speed = 2;
        [SerializeField] private GameObject fx;

        private TickData tick = new TickData();

        public abstract void Set(HandType type);

        private void OnDisable()
        {
            tick.RemoveTick();
        }

        public void Move(Transform end, Transform start, bool isLoop = false)
        {
            fx.SetActive(true);

            tick.Action = () => Tick(end, start, isLoop);
            tick.RegisterTick();
        }

        private void Tick(Transform end, Transform start, bool isLoop)
        {
            float step = speed * Time.fixedDeltaTime;
            this.gameObject.transform.position = Vector3.MoveTowards(transform.position, end.position, step);

            if (transform.position.SqrMagnitude(end.position) < 0.001f)
            {
                if (isLoop)
                {
                    fx.SetActive(false);
                    this.transform.position = start.position;
                    fx.SetActive(true);
                }
                else
                {
                    tick.RemoveTick();
                }
            }
        }
    }
}