using System;

public class SimpleProjectileHitBehaviour : IProjectileHitBehaviour
{
    private readonly IProjectileHitController _controller;
    private readonly Settings _settings;

    public SimpleProjectileHitBehaviour(IProjectileHitController controller, Settings settings)
    {
        _controller = controller;
        _settings = settings;
    }

    public void OnHit(Projectile proj, IDamageable enemy)
    {
        _controller.HandleHit(proj, enemy);
    }

    [Serializable]
    public class Settings : IProjectileHitBehaviourSettings
    {

    }
}