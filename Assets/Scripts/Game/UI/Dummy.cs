using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public abstract class Dummy : MonoBehaviour
{
    public abstract void Set(string content);

    public virtual void Loot(Transform start, Transform end, Action action = null, float delay = 0.4f)
    {
        this.transform.position = start.position;
        this.transform.localScale = new Vector3(1, 1, 1);

        this.transform.DOJump(start.position + new Vector3(Random.Range(0.15f, 0.85f) * (Random.Range(0, 2) == 0 ? 0.5f : -0.5f), Random.Range(0.15f, 0.85f) * (Random.Range(0, 2) == 0 ? 1 : -1)), 3, 1, 0.5f).SetEase(Ease.OutQuad).OnComplete(() =>
        {
            this.transform.DOMove(transform.position, delay).OnComplete(() =>
            {
                this.transform.SetParent(end);
                this.transform.localScale = new Vector3(1, 1, 1);

                this.transform.DOLocalJump(Vector3.zero, 0.5f, 1, Random.Range(0.5f, 1.0f)).SetEase(Ease.InQuad).OnComplete(() =>
                {
                    this.transform.localScale = new Vector3(1, 1, 1);
                    NPS.Pooling.Manager.S.Despawn(this);

                    action?.Invoke();
                });
            });
        });
    }
}
