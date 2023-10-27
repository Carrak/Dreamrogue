using UnityEngine;

public delegate void OnWeaponStateChangedHandler(WeaponState state);
public delegate void OnApplyCooldownHandler(float seconds);
public delegate void OnAttackSequenceStateChangedHandler(bool state);

public interface IAttackSequence 
{
    public event OnWeaponStateChangedHandler StateChanged;
    public event OnApplyCooldownHandler ApplyCooldown;
    public event OnAttackSequenceStateChangedHandler OnAttackSequenceStateChanged;

    void Press(Transform t);
    void Hold(Transform t);
    void Release(Transform t);
}