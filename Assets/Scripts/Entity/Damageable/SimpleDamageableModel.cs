using System;
using UnityEngine;

public class SimpleDamageableModel : IDamageableModel
{
    public float MaxHP => _settings.MaxHP;

    public float CurrentHP { get; private set; }
    public bool IsAlive { get; private set; }

    public event OnDeathHandler OnDeath;
    public event OnDamageTakenHandler OnDamageTaken;

    private readonly Settings _settings;

    public SimpleDamageableModel(Settings settings)
    {
        _settings = settings;

        CurrentHP = _settings.MaxHP;
        IsAlive = true;
    }

    public void ReceiveDamageInstance(DamageInstance damage)
    {
        if (!IsAlive)
            return;

        CurrentHP -= damage.Amount;

        if (CurrentHP <= 0)
        {
            IsAlive = false;
            OnDeath?.Invoke();
        }

        OnDamageTaken?.Invoke(damage);

        Debug.Log($"TOOK {damage.Amount} DAMAGE | REMAINING HP: {CurrentHP} / {MaxHP}");
    }

    [Serializable]
    public class Settings
    {
        public float MaxHP;
    }
}