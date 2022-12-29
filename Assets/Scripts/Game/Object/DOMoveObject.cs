using DG.Tweening;
using NPS.Math;
using UnityEngine;
using UnityEngine.Events;

public class DOMoveObject : MonoBehaviour
{
    [SerializeField] private UnityEvent OnComplete;

    public void Set(Vector3 from, Vector3 to, float speed, bool direction = true)
    {
        this.transform.position = from;

        if (direction)
        {
            float angle = from.AngleTo(to);
            this.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }

        float dis = from.SqrMagnitude(to);
        dis = Mathf.Sqrt(dis);
        this.transform.DOMove(to, dis / speed).SetEase(Ease.Linear).OnComplete(Complete);
    }

    private void Complete()
    {
        OnComplete?.Invoke();
    }
}
