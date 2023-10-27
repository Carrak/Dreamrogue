using UnityEngine;
using Zenject;

[CreateAssetMenu(menuName = "Dreamrogue/Weapon settings")]
public class WeaponSettingsInstaller : ScriptableObjectInstaller<WeaponSettingsInstaller>
{
    public MeleeWeapon.Settings BasicSword;
    public CrimsonDagger.Settings CrimsonDagger;

    public override void InstallBindings()
    {
        Container.BindInstance(BasicSword).WhenInjectedInto<BasicSword>();
        Container.BindInstance(CrimsonDagger);
    }
}