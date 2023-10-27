public interface IDamageable
{
    int Id { get; }

    void ReceiveDamageInstance(DamageInstance damage);
}