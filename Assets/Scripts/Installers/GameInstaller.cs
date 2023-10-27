using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller
{
    [SerializeField]
    private GameObject slimePrefab;

    [SerializeField]
    private GameObject playerPrefab;

    [SerializeField]
    private GameObject pickableWeaponPrefab;

    [SerializeField]
    private GameObject projectilePrefab;

    [SerializeField]
    private DebugSpawner spawner;

    public override void InstallBindings()
    {
        Container.BindFactory<Projectile.Settings,
            IProjectileMovementSettings, IProjectileHitBehaviourSettings,
            Projectile, Projectile.Factory>()
            .FromSubContainerResolve()
            .ByNewPrefabInstaller<ProjectileInstaller>(projectilePrefab);

        Container.BindFactory<SlimeView, SlimeView.Factory>()
            .FromSubContainerResolve()
            .ByNewPrefabInstaller<SlimeInstaller>(slimePrefab)
            .WithGameObjectName("Slime");

        Container.BindFactory<IWeapon, PickableWeapon, PickableWeapon.Factory>()
            .FromComponentInNewPrefab(pickableWeaponPrefab);

        Container.Bind<PlayerView>()
            .FromSubContainerResolve()
            .ByNewPrefabInstaller<PlayerInstaller>(playerPrefab)
            .AsSingle().NonLazy(); 
        //Container.Bind<WeaponModel>().FromSubContainerResolve().ByNewContextPrefab(playerPrefab).AsSingle().NonLazy();

        Container.BindInstances(spawner);

        WeaponsInstaller.Install(Container);
    }
}
