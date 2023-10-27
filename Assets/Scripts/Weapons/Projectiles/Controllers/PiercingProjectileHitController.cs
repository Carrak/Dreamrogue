public class PiercingProjectileHitController : IProjectileHitController
{
    private readonly IProjectileModel _model;
    private readonly ProjectilePierceTracker _piercing;

    public PiercingProjectileHitController(ProjectilePierceTracker piercing, IProjectileModel projectileModel)
    {
        _piercing = piercing;
        _model = projectileModel;
    }

    public void HandleHit(Projectile proj, IDamageable enemy)
    {
        if (!_piercing.CanHitEnemy(enemy))
            return;

        enemy.ReceiveDamageInstance(new DamageInstance(proj.transform.up, _model.DamageParams));

        _piercing.AddHitEnemy(enemy);
    }
}