public struct DamageParams
{
    public float KnockbackCoef { get; }
    public float Amount { get; }

    public DamageParams(float knockbackCoef, float amount)
    {
        KnockbackCoef = knockbackCoef;
        Amount = amount;
    }

    public DamageParams(DamageParamsRaw raw)
    {
        KnockbackCoef = raw.KnockbackCoef;
        Amount = raw.Amount;
    }
}