using System;

public class CrimsonDagger : MeleeWeapon
{
    public CrimsonDagger(MeleeWeapon.Settings s, CrimsonDaggerNormalAttack normal, CrimsonDaggerChargedAttack charged) :
        base(s, normal, charged)
    {
    }

    [Serializable]
    public new class Settings
    {
        public MeleeWeapon.Settings MeleeSettings;
        public Projectile.Settings ProjectileSettings;
        public ControllableProjectileMovement.Settings MovementSettings;
        public SimpleProjectileHitBehaviour.Settings HitBehaviourSettings;
    }
}