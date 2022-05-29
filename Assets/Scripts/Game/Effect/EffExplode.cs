using MEC;
using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffExplode : MonoBehaviour
{
    private IOverlap overlapCircle;

    private void Awake()
    {
        overlapCircle = GetComponent<IOverlap>();
    }

    public void SetDmg(float dmg)
    {
        overlapCircle.SetDamage(dmg);
    }

    public void SetForce(float force)
    {
        overlapCircle.SetExplosionForce(force);
    }

    public void SetIce(float time)
    {
        overlapCircle.SetIce(time);
    }

    public void Ray()
    {
        overlapCircle.Ray();
    }
}
