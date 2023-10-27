using Zenject;

public class WeaponsInstaller : Installer<WeaponsInstaller>
{
    public override void InstallBindings()
    {
        Container.Bind<BasicSword>().FromSubContainerResolve().ByMethod(BasicSword).AsSingle();
        Container.Bind<CrimsonDagger>().FromSubContainerResolve().ByInstaller<CrimsonDaggerInstaller>().AsSingle();
    }

    private void BasicSword(DiContainer sub)
    {
        sub.Bind<BasicSwordNormalAttack>().AsSingle();
        sub.Bind<BasicSwordChargedAttack>().AsSingle();
        sub.Bind<BasicSword>().AsSingle();
    }
}