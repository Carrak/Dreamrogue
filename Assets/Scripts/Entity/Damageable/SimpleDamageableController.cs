using UnityEngine;

public class SimpleDamageableController : IDamageableController
{
    private readonly IDamageableModel _model;

    public SimpleDamageableController(IDamageableModel model)
    {
        _model = model;
    }

    public void HandleDamageReceived(DamageInstance damage)
    {
        _model.ReceiveDamageInstance(damage);
    }
}