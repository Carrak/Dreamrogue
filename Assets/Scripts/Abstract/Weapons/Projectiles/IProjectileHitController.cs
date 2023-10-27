public interface IProjectileHitController
{
    void HandleHit(Projectile proj, IDamageable enemy);
}