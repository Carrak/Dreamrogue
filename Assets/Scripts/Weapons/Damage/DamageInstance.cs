using UnityEngine;

public struct DamageInstance
{
    public Vector2 KnockbackDirection { get; }
    public float Amount { get; }

    public DamageInstance(Vector2 direction, DamageParams damageParams)
    {
        KnockbackDirection = direction * damageParams.KnockbackCoef;
        Amount = damageParams.Amount;
    }
}