using UnityEngine;

public delegate void AttackingStateChangedHandler(AttackingState attackingState);

public interface IWeapon
{
    Sprite Sprite { get; }

    WeaponState WeaponState { get; }
    AttackingState AttackingState { get; }

    DamageParams GetDamageParams();

    event OnWeaponStateChangedHandler OnWeaponStateChanged;
    event AttackingStateChangedHandler OnAttackingStateChanged;

    void PressNormal(Transform t);
    void HoldNormal(Transform t);
    void ReleaseNormal(Transform t);
    void PressSpecial(Transform t);
    void HoldSpecial(Transform t);
    void ReleaseSpecial(Transform t);
}