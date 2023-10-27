using UnityEngine;

public interface IProjectileHitBehaviour
{
    public void OnHit(Projectile proj, IDamageable enemy);
}

public interface IProjectileHitBehaviourSettings
{

}