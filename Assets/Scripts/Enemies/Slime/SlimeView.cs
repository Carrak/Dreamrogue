using UnityEngine;
using Zenject;
using DG.Tweening;

public class SlimeView : MonoBehaviour, IDamageable
{
    private FlashEffect _damageFlash;

    private SlimeStateManager _stateManager;
    private IDamageableController _controller;
    private IDamageableModel _model;

    public int Id => GetInstanceID();

    [Inject]
    public void Inject(FlashEffect flash, SlimeStateManager stateManager, IDamageableController controller, IDamageableModel model)
    {
        _damageFlash = flash;
        _stateManager = stateManager;
        _controller = controller;
        _model = model;
    }

    private void OnEnable()
    {
        _model.OnDamageTaken += OnDamageTaken;
    }

    private void OnDisable()
    {
        _model.OnDamageTaken -= OnDamageTaken;
    }

    private void Update()
    {
        _stateManager.Update();
    }

    public void ReceiveDamageInstance(DamageInstance damage)
    {
        _controller.HandleDamageReceived(damage);
    }

    private void OnDamageTaken(DamageInstance damage)
    {
        transform.DOBlendableMoveBy(damage.KnockbackDirection, 0.2f).SetEase(Ease.OutSine);
        _damageFlash.DoFlash();
    }

    public class Factory : PlaceholderFactory<SlimeView> { }
}