using UnityEngine;
using Zenject;

[CreateAssetMenu(menuName = "Dreamrogue/Entity settings")]
public class EntitySettingsInstaller : ScriptableObjectInstaller<EntitySettingsInstaller>
{
    public PlayerInstaller.Settings Player;
    public SlimeInstaller.Settings Slime;

    public FlashEffect.Settings GlobalDamageTakenFlashEffect;

    public override void InstallBindings()
    {
        Container.BindInstance(Player).IfNotBound();
        Container.BindInstance(Slime).IfNotBound();
        Container.BindInstance(GlobalDamageTakenFlashEffect).IfNotBound();
    }
}