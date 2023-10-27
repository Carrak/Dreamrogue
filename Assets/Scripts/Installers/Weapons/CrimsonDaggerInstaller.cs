using UnityEngine;
using Zenject;

public class CrimsonDaggerInstaller : Installer<CrimsonDagger.Settings, CrimsonDaggerInstaller>
{
    private readonly CrimsonDagger.Settings _settings;

    public CrimsonDaggerInstaller(CrimsonDagger.Settings settings)
    {
        _settings = settings;
    }

    public override void InstallBindings()
    {
        Container.BindInstance(_settings.MeleeSettings);

        Container.Bind<ProjectilePool>()
            .AsSingle()
            .WithArguments(_settings.ProjectileSettings, _settings.MovementSettings, _settings.HitBehaviourSettings,
            "CrimsonDaggerProjectile", 4, 4
            );

        Container.Bind<CrimsonDaggerNormalAttack>().AsSingle();
        Container.Bind<CrimsonDaggerChargedAttack>().AsSingle();
        Container.Bind<CrimsonDagger>().AsSingle();
    }
}