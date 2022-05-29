public interface IOverlap
{
    void SetDamage(DamageData damage);

    void SetDamage(float dmg);

    void SetExplosionForce(ExplosionForceData explosionForce);

    void SetExplosionForce(float force);

    void SetIce(IceData ice);

    void SetIce(float time);

    void Ray();
}
