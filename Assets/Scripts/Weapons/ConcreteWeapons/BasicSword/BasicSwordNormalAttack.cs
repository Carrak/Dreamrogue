using DG.Tweening;
using UnityEngine;

public class BasicSwordNormalAttack : IAttackSequence
{
    public event OnWeaponStateChangedHandler StateChanged;
    public event OnApplyCooldownHandler ApplyCooldown;
    public event OnAttackSequenceStateChangedHandler OnAttackSequenceStateChanged;

    public void Press(Transform t)
    {
        OnAttackSequenceStateChanged.Invoke(true);
        var seq = DOTween.Sequence();

        float windUpTime = 0.2f;
        seq.AppendCallback(() => StateChanged.Invoke(WeaponState.WindUp));
        seq.Append(t.DOLocalMove(new Vector3(-0.8f, -0.3f, 0), windUpTime).SetEase(Ease.OutSine));
        seq.Join(t.DOLocalRotate(new Vector3(0, 0, 50), windUpTime).SetEase(Ease.OutSine));
        
        float attackTime = 0.1f;
        seq.AppendCallback(() => StateChanged.Invoke(WeaponState.Attack));
        seq.Append(t.DOLocalMove(new Vector3(0.8f, -0.3f, 0), attackTime).SetEase(Ease.OutSine));
        seq.Join(t.DOLocalRotate(new Vector3(0, 0, -30), attackTime).SetEase(Ease.OutSine));

        float returnTime = 0.15f;
        seq.AppendCallback(() => StateChanged.Invoke(WeaponState.ReturningToIdle));
        seq.Append(t.DOLocalMove(new Vector3(0, 0, 0), returnTime).SetEase(Ease.OutSine));
        seq.Join(t.DOLocalRotate(new Vector3(0, 0, 0), returnTime).SetEase(Ease.OutSine));
        seq.AppendCallback(() => StateChanged.Invoke(WeaponState.Idle));

        seq.AppendCallback(() => OnAttackSequenceStateChanged.Invoke(false));
    }

    public void Hold(Transform t)
    {
        
    }

    public void Release(Transform t)
    {
        
    }
}