public class SimpleProjectileModel : IProjectileModel
{
    public DamageParams DamageParams { get; }

    public SimpleProjectileModel(DamageParamsRaw damageParams)
    {
        DamageParams = new DamageParams(damageParams);
    }
}