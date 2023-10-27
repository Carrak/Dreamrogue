using System.Collections.Generic;

public class WeaponModel : IWeaponModel
{
    public IWeapon HeldWeapon { get; private set; }

    public bool CanPoint =>
        HeldWeapon.WeaponState == WeaponState.Idle ||
        HeldWeapon.WeaponState == WeaponState.Charging ||
        HeldWeapon.WeaponState == WeaponState.Charged;

    public bool CanHit => HeldWeapon.WeaponState == WeaponState.Attack;

    public AttackingState AttackingState { get; private set; }

    public event OnWeaponEquippedHandler OnWeaponEquipped;
    public event OnPressNormalHandler OnPressNormal;
    public event OnHoldNormalHandler OnHoldNormal;
    public event OnReleaseNormalHandler OnReleaseNormal;
    public event OnPressSpecialHandler OnPressSpecial;
    public event OnHoldSpecialHandler OnHoldSpecial;
    public event OnReleaseSpecialHandler OnReleaseSpecial;

    public event OnWeaponStateChangedHandler OnStateChanged;

    private readonly HashSet<int> _hits = new();

    public bool HasHitEnemy(IDamageable enemy) => _hits.Contains(enemy.Id);

    public void AddHitEnemy(IDamageable enemy) => _hits.Add(enemy.Id);

    public DamageParams GetDamageParams() => HeldWeapon.GetDamageParams();

    public void EquipWeapon(IWeapon weapon)
    {
        if (HeldWeapon != null)
        {
            HeldWeapon.OnWeaponStateChanged -= StateChanged;
            HeldWeapon.OnAttackingStateChanged -= AttackingStateChanged;
        }

        var old = HeldWeapon;
        HeldWeapon = weapon;

        if (HeldWeapon != null)
        {
            HeldWeapon.OnWeaponStateChanged += StateChanged;
            HeldWeapon.OnAttackingStateChanged += AttackingStateChanged;
        }

        OnWeaponEquipped?.Invoke(old, HeldWeapon);
    }

    private void AttackingStateChanged(AttackingState attackingState) => AttackingState = attackingState;

    private void StateChanged(WeaponState state)
    {
        _hits.Clear();
        OnStateChanged?.Invoke(state);
    }

    public void PressNormal()
    {
        OnPressNormal?.Invoke();
    }

    public void HoldNormal()
    {
        OnHoldNormal?.Invoke();
    }

    public void ReleaseNormal()
    {
        OnReleaseNormal?.Invoke();
    }

    public void PressSpecial()
    {
        OnPressSpecial?.Invoke();
    }

    public void HoldSpecial()
    {
        OnHoldSpecial?.Invoke();
    }

    public void ReleaseSpecial()
    {
        OnReleaseSpecial?.Invoke();
    }
}