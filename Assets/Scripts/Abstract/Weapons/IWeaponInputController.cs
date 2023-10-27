using UnityEngine;

public interface IWeaponInputController
{
    void Update();

    void HandleHit(IDamageable enemy, Vector2 direction);
} 