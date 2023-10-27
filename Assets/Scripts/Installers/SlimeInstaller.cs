using System;
using UnityEngine;
using Zenject;

public class SlimeInstaller : Installer<SlimeInstaller>
{
    public override void InstallBindings()
    {
        Container.BindInterfacesAndSelfTo<SlimeView>().FromComponentInHierarchy().AsSingle();

        // movement
        Container.BindInterfacesTo<SimpleMovementModel>().AsSingle();
        Container.Bind<SimpleMovementModel.Settings>().FromResolveGetter<Settings>(x => x.Movement);
        Container.BindInterfacesTo<ProximityMovementController>().AsSingle();

        // damageable
        Container.BindInterfacesTo<SimpleDamageableModel>().AsSingle();
        Container.Bind<SimpleDamageableModel.Settings>().FromResolveGetter<Settings>(x => x.Damageable);
        Container.BindInterfacesTo<SimpleDamageableController>().AsSingle();
        Container.Bind<FlashEffect>().AsSingle();

        // components
        Container.Bind<Animator>().FromComponentInHierarchy().AsSingle();
        Container.Bind<SpriteRenderer>().FromComponentInHierarchy().AsSingle();

        Container.Bind<Transform>()
            .FromResolveGetter<SlimeView>(x => x.transform).AsSingle()
            .WhenInjectedInto(typeof(IMovementModel), typeof(ProximityMovementController));

        // state
        Container.BindInterfacesAndSelfTo<SlimeStateManager>().AsSingle();
        Container.Bind<SlimeAliveState>().AsSingle();
        Container.Bind<SlimeDyingState>().AsSingle();
    }

    [Serializable]
    public class Settings
    {
        public SimpleMovementModel.Settings Movement;
        public SimpleDamageableModel.Settings Damageable;
    }
}