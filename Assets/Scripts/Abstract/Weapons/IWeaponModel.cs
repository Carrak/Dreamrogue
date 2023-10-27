using System;
using UnityEngine;

public delegate void OnWeaponEquippedHandler(IWeapon oldWeapon, IWeapon newWeapon);

public delegate void OnPressNormalHandler();
public delegate void OnHoldNormalHandler();
public delegate void OnReleaseNormalHandler();
public delegate void OnPressSpecialHandler();
public delegate void OnHoldSpecialHandler();
public delegate void OnReleaseSpecialHandler();

public interface IWeaponModel
{
    bool CanPoint { get; }
    bool CanHit { get; }

    AttackingState AttackingState { get; }

    IWeapon HeldWeapon { get; }

    event OnWeaponEquippedHandler OnWeaponEquipped;
    event OnWeaponStateChangedHandler OnStateChanged;

    event OnPressNormalHandler OnPressNormal;
    event OnHoldNormalHandler OnHoldNormal;
    event OnReleaseNormalHandler OnReleaseNormal;
    event OnPressSpecialHandler OnPressSpecial;
    event OnHoldSpecialHandler OnHoldSpecial;
    event OnReleaseSpecialHandler OnReleaseSpecial;

    DamageParams GetDamageParams();
    void EquipWeapon(IWeapon weapon);

    bool HasHitEnemy(IDamageable enemy);
    void AddHitEnemy(IDamageable enemy);

    void PressNormal();
    void HoldNormal();
    void ReleaseNormal();
    void PressSpecial();
    void HoldSpecial();
    void ReleaseSpecial();
}

public enum AttackingState
{
    NotAttacking,
    AttackingNormal,
    AttackingSpecial
}
