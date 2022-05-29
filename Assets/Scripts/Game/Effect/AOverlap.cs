using MEC;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AOverlap<T> : MonoBehaviour, IOverlap
{
    [SerializeField] private float delay = 0.2f;

    [SerializeField] private bool inMain = false;
    [SerializeField] protected LayerBit[] layers;
    [SerializeField] private List<ObjectTag> tags;

    [SerializeField] private bool isExplosionForce = false;
    [SerializeField] private ExplosionForceData explosionForce;

    [SerializeField] private bool isDamage = false;
    [SerializeField] private DamageData damage;

    [SerializeField] private bool isFreeze = false;
    [SerializeField] private IceData ice;

    private CoroutineHandle handle;

    public void SetDamage(DamageData damage)
    {
        isDamage = true;
        this.damage = damage;
    }

    public void SetDamage(float dmg)
    {
        isDamage = true;
        damage.Value = dmg;
    }

    public void SetExplosionForce(ExplosionForceData explosionForce)
    {
        isExplosionForce = true;
        this.explosionForce = explosionForce;
    }

    public void SetExplosionForce(float force)
    {
        isExplosionForce = true;
        explosionForce.Value = force;
    }

    public void SetIce(IceData ice)
    {
        isFreeze = true;
        this.ice = ice;
    }

    public void SetIce(float time)
    {
        isFreeze = true;
        ice.Value = time;
    }

    public void Ray()
    {
        handle = Timing.RunCoroutine(_Ray());
    }

    private void OnDisable()
    {
        if (handle.IsValid) Timing.KillCoroutines(handle);
    }

    protected abstract Collider2D[] GetCollider();

    private IEnumerator<float> _Ray()
    {
        yield return Timing.WaitForSeconds(delay);

        var cl = GetCollider();
        var objs = cl.GetObject<T>(tags, inMain);

        foreach (var obj in objs)
        {
            if (isDamage)
            {
                IHit iHit = obj.GetGameObject().GetComponent<IHit>();
                iHit?.Hit(damage);
            }

            if (isExplosionForce)
            {
                explosionForce.Form = this.transform.position;

                IExplosionForce iEx = obj.GetGameObject().GetComponent<IExplosionForce>();
                iEx?.ExplosionForce(explosionForce);
            }

            if (isFreeze)
            {
                IFreeze iFz = obj.GetGameObject().GetComponent<IFreeze>();
                iFz?.Freeze(ice);
            }
        }
    }    
}
