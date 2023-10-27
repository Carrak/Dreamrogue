using System;
using UnityEngine;

public abstract class MeleeWeapon : IMelee
{
    public WeaponState WeaponState { get; private set; }
    public AttackingState AttackingState { get; private set; }

    public ColliderBorders Hitbox => _settings.Hitbox;
    public Sprite Sprite => _settings.Sprite;

    private readonly Settings _settings;

    protected readonly IAttackSequence _normal;
    protected readonly IAttackSequence _special;

    public event OnWeaponStateChangedHandler OnWeaponStateChanged;
    public event AttackingStateChangedHandler OnAttackingStateChanged;

    public MeleeWeapon(Settings settings, IAttackSequence normal, IAttackSequence special)
    {
        _settings = settings;
        _normal = normal;
        _special = special;

        _normal.StateChanged += ChangeWeaponState;
        _normal.OnAttackSequenceStateChanged += NormalIsAttacking;

        _special.StateChanged += ChangeWeaponState;
        _special.OnAttackSequenceStateChanged += ChargedIsAttacking;
    }

    private void NormalIsAttacking(bool attacking)
    {
        AttackingState = attacking ? AttackingState.AttackingNormal : AttackingState.NotAttacking;
        OnAttackingStateChanged?.Invoke(AttackingState);
    }
    private void ChargedIsAttacking(bool attacking)
    {
        AttackingState = attacking ? AttackingState.AttackingSpecial : AttackingState.NotAttacking;
        OnAttackingStateChanged?.Invoke(AttackingState);
    }

    private void ChangeWeaponState(WeaponState state)
    {
        WeaponState = state;
        OnWeaponStateChanged.Invoke(state);
    }

    public DamageParams GetDamageParams()
    {
        return AttackingState switch
        {
            AttackingState.AttackingNormal => new DamageParams(_settings.NormalAttackDamage),
            AttackingState.AttackingSpecial => new DamageParams(_settings.ChargedAttackDamage),
            _ => new DamageParams()
        };
    }

    public void PressNormal(Transform t) => _normal.Press(t);
    public void HoldNormal(Transform t) => _normal.Hold(t);
    public void ReleaseNormal(Transform t) => _normal.Release(t);
    public void PressSpecial(Transform t) => _special.Press(t);
    public void HoldSpecial(Transform t) => _special.Hold(t);
    public void ReleaseSpecial(Transform t) => _special.Release(t);

    [Serializable]
    public class Settings
    {
        public DamageParamsRaw NormalAttackDamage;
        public DamageParamsRaw ChargedAttackDamage;
        public ColliderBorders Hitbox;
        public Sprite Sprite;
    }
}