public class SimpleProjectileHitController : IProjectileHitController
{
    private readonly SimpleProjectileModel _model;

    public SimpleProjectileHitController(SimpleProjectileModel model)
    {
        _model = model;
    }

    public void HandleHit(Projectile proj, IDamageable enemy)
    {
        enemy.ReceiveDamageInstance(new DamageInstance(proj.transform.up, _model.DamageParams));
        proj.ReturnToQueue();
    }

    public void OnProjectileDespawned()
    {
        // do nothing
    }
}