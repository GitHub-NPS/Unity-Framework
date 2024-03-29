using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleMotion : MonoBehaviour
{
    [SerializeField] private CheckType check;
    [SerializeField] private int count = 1;

    [SerializeField] private Vector3 start = new Vector3(0, 0, 0);
    [SerializeField] private Vector3 finish = new Vector3(1f, 1f, 1f);

    [SerializeField] private float time = 0.1f;

    private int c = 0;
    private Tween tw = default;

    private void OnEnable()
    {
        c = 0;

        if (tw != default) tw.Kill();
        Motion();
    }

    private void OnDisable()
    {
        if (tw != default) tw.Kill();
    }

    private void Motion()
    {
        this.transform.localScale = start;
        tw = this.transform.DOScale(finish, time).OnComplete(() =>
        {
            c++;
            if ((!(check == CheckType.Time && count == 1) && c < count) || check == CheckType.Forever)
            {
                this.transform.localScale = finish;
                this.transform.DOScale(start, time).OnComplete(() =>
                {
                    Motion();
                }).SetUpdate(true);
            }
        }).SetUpdate(true);
    }
}
