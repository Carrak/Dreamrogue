using UnityEngine;

public class WeaponController : IWeaponInputController
{
    private readonly IWeaponModel _model;

    public WeaponController(IWeaponModel model)
    {
        _model = model;
    }

    public void Update()
    {
        if (_model.HeldWeapon is null)
            return;

        switch(_model.AttackingState)
        {
            case AttackingState.NotAttacking:
                if (Input.GetMouseButtonDown(0))
                    _model.PressNormal();
                else if (Input.GetMouseButtonDown(1))
                    _model.PressSpecial();
                break;
            case AttackingState.AttackingNormal:
                if (Input.GetMouseButton(0))
                    _model.HoldNormal();
                if (Input.GetMouseButtonUp(0))
                    _model.ReleaseNormal();
                break;
            case AttackingState.AttackingSpecial:
                if (Input.GetMouseButton(1))
                    _model.HoldSpecial();
                if (Input.GetMouseButtonUp(1))
                    _model.ReleaseSpecial();
                break;
        }
    }

    public void HandleHit(IDamageable enemy, Vector2 direction)
    {
        if (!_model.CanHit || _model.HasHitEnemy(enemy))
            return;

        var dmgParams = _model.GetDamageParams();
        enemy.ReceiveDamageInstance(new DamageInstance(direction, dmgParams));

        _model.AddHitEnemy(enemy);
    }
}