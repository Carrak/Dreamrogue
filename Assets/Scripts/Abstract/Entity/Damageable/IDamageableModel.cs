using UnityEngine;

public delegate void OnDeathHandler();
public delegate void OnDamageTakenHandler(DamageInstance damage);

public interface IDamageableModel
{
    float MaxHP { get; }
    float CurrentHP { get; }
    bool IsAlive { get; }

    void ReceiveDamageInstance(DamageInstance instance);

    public event OnDeathHandler OnDeath;
    public event OnDamageTakenHandler OnDamageTaken;
}