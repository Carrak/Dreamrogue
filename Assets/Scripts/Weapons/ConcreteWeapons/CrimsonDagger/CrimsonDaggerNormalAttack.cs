using DG.Tweening;
using UnityEngine;

public class CrimsonDaggerNormalAttack : IAttackSequence
{
    public event OnWeaponStateChangedHandler StateChanged;
    public event OnApplyCooldownHandler ApplyCooldown;
    public event OnAttackSequenceStateChangedHandler OnAttackSequenceStateChanged;

    public void Hold(Transform t)
    {
        if (DOTween.IsTweening("dagger" + t.GetInstanceID()))
            return;
        
        var seq = DOTween.Sequence();
        seq.SetId("dagger" + t.GetInstanceID());

        float attackTime = 0.03f;
        seq.AppendCallback(() => StateChanged.Invoke(WeaponState.Attack));
        seq.Append(t.DOLocalMove(new Vector3(0, 0.6f, 0), attackTime).SetEase(Ease.InSine));

        float backTime = 0.1f;
        seq.AppendCallback(() => StateChanged.Invoke(WeaponState.ReturningToIdle));
        seq.Append(t.DOLocalMove(new Vector3(0, -0.2f, 0), backTime).SetEase(Ease.OutSine));

        float returnTime = 0.07f;
        seq.Append(t.DOLocalMove(new Vector3(0, 0, 0), returnTime).SetEase(Ease.InSine));

        seq.AppendCallback(() => StateChanged.Invoke(WeaponState.Idle));
    }

    public void Press(Transform t)
    {
        OnAttackSequenceStateChanged.Invoke(true);
    }

    public void Release(Transform t)
    {
        OnAttackSequenceStateChanged.Invoke(false);
    }
}
