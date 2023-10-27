using Zenject;

public class ProjectileInstaller : Installer<Projectile.Settings, 
    IProjectileMovementSettings, 
    IProjectileHitBehaviourSettings, 
    ProjectileInstaller>
{
    private readonly IProjectileHitBehaviourSettings _hitBehaviourSettings;
    private readonly IProjectileMovementSettings _movementSettings;
    private readonly Projectile.Settings _settings;

    public ProjectileInstaller(Projectile.Settings settings, 
        IProjectileMovementSettings movement, 
        IProjectileHitBehaviourSettings hitBehaviour)
    {
        _settings = settings;
        _movementSettings = movement;
        _hitBehaviourSettings = hitBehaviour;
    }

    public override void InstallBindings()
    {
        Container.BindInstance(_settings);

        Container.Bind<Projectile>().FromComponentInHierarchy().AsSingle();
        Container.Bind<LifetimeController>().AsSingle();
        Container.Bind<LifetimeModel>().AsSingle().WithArguments(_settings.Lifetime);

        Container.BindInterfacesAndSelfTo<SimpleProjectileModel>().AsSingle().WithArguments(_settings.Damage);

        // controller
        if (_settings.IsPiercing)
        {
            Container.Bind<ProjectilePierceTracker>().AsSingle().WithArguments(_settings.PiercingHitCooldown);
            Container.BindInterfacesAndSelfTo<PiercingProjectileHitController>().AsSingle();
        }
        else
        {
            Container.BindInterfacesAndSelfTo<SimpleProjectileHitController>().AsSingle();
        }

        // hit behaviour
        switch (_hitBehaviourSettings)
        {
            case SimpleProjectileHitBehaviour.Settings s:
                Container.BindInterfacesTo<SimpleProjectileHitBehaviour>().AsSingle().WithArguments(s);
                break;
            // exploding type
        }

        // movement type
        switch (_movementSettings)
        {
            case ControllableProjectileMovement.Settings s:
                Container.BindInterfacesTo<ControllableProjectileMovement>().AsSingle().WithArguments(s);
                break;
            // other types
        }





    }
}