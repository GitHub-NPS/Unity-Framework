using MEC;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverlapCircle<T> : AOverlap<T>
{
    [SerializeField] private Vector3 offset;
    [SerializeField] private float radius = 2.5f;

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(this.transform.position + offset, radius);
    }
#endif

    protected override Collider2D[] GetCollider()
    {
        return Physics2D.OverlapCircleAll(this.transform.position + offset, radius, Utils.FindLayer(layers));
    }
}
