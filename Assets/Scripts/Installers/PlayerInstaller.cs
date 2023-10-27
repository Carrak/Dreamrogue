using System;
using UnityEngine;
using Zenject;

public class PlayerInstaller : Installer<PlayerInstaller.Settings, PlayerInstaller>
{
    private readonly Settings _settings;

    public PlayerInstaller(Settings settings) => _settings = settings;

    public override void InstallBindings()
    {
        Container.Bind<PlayerView>().FromComponentInHierarchy().AsSingle();

        // movement
        Container.BindInterfacesTo<SimpleMovementModel>().AsSingle();
        Container.BindInterfacesTo<KeyboardMovementController>().AsSingle();
        Container.Bind<SimpleMovementModel.Settings>().FromResolveGetter<Settings>(x => x.Movement);

        // dash
        Container.Bind<PlayerDashModel>().AsSingle().WithArguments(_settings.Dash);
        Container.Bind<PlayerDashController>().AsSingle();
        Container.BindInterfacesAndSelfTo<PrefabPool<SpriteRenderer>>().AsSingle().WithArguments(
            _settings.DashTrailPrefab, "DashTrail", 
            _settings.Dash.Afterimages * 2, _settings.Dash.Afterimages
            ).WhenInjectedInto<PlayerDashingState>();

        // weapon
        Container.Bind<PlayerWeaponView>().FromComponentInHierarchy().AsSingle();
        Container.BindInterfacesAndSelfTo<WeaponModel>().AsSingle();
        Container.BindInterfacesTo<WeaponController>().AsSingle();
        Container.Bind<WeaponView>().FromComponentInHierarchy().AsSingle();
        Container.Bind<FlashEffect>().AsSingle().WithArguments(_settings.FlashEffectOnChargedAttack);

        // items
        Container.Bind<ProximityItemController>().AsSingle();
        Container.Bind<ProximityItemModel>().AsSingle();

        // components
        Container.Bind<SpriteRenderer>()
            .FromResolveGetter<WeaponView>(x => x.GetComponent<SpriteRenderer>())
            .WhenInjectedInto(typeof(FlashEffect));
        Container.Bind<SpriteRenderer>()
            .FromResolveGetter<PlayerView>(x => x.GetComponent<SpriteRenderer>())
            .WhenInjectedInto(typeof(PlayerDashingState), typeof(IMovementModel));
        Container.Bind<Transform>()
            .FromResolveGetter<PlayerView>(x => x.transform)
            .WhenInjectedInto(typeof(PlayerMovingState), typeof(PlayerDashingState), typeof(ProximityItemController));
        Container.Bind<Transform>()
            .FromResolveGetter<WeaponView>(x => x.transform)
            .WhenInjectedInto<WeaponModel>();

        // state
        Container.BindInterfacesAndSelfTo<PlayerStateManager>().AsSingle();
        Container.Bind<PlayerIdleState>().AsSingle();
        Container.Bind<PlayerMovingState>().AsSingle();
        Container.Bind<PlayerDashingState>().AsSingle();
    }

    [Serializable]
    public class Settings
    {
        public SimpleMovementModel.Settings Movement;
        public PlayerDashModel.Settings Dash;
        public GameObject DashTrailPrefab;
        public FlashEffect.Settings FlashEffectOnChargedAttack;
    }
}