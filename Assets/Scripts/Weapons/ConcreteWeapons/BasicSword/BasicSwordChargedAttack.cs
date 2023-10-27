using DG.Tweening;
using UnityEngine;

public class BasicSwordChargedAttack : IAttackSequence
{
    public event OnWeaponStateChangedHandler StateChanged;
    public event OnApplyCooldownHandler ApplyCooldown;
    public event OnAttackSequenceStateChangedHandler OnAttackSequenceStateChanged;

    private float chargeStarted;
    private bool hasCharged;

    private readonly float needsForCharge = 0.7f;

    public void Hold(Transform t)
    {
        if (hasCharged || chargeStarted == -1)
            return;

        float progress = Mathf.Min((Time.time - chargeStarted) / needsForCharge, 1);

        if (progress == 1)
        {
            hasCharged = true;
            StateChanged.Invoke(WeaponState.Charged);
        }

        t.localPosition = progress * new Vector3(-0.8f, -1f, 0);
        t.localEulerAngles = progress * new Vector3(0, 0, 100);
    }

    public void Press(Transform t)
    {
        OnAttackSequenceStateChanged.Invoke(true);
        StateChanged.Invoke(WeaponState.Charging);
        chargeStarted = Time.time;
    }

    public void Release(Transform t)
    {
        float attackTime = 0.15f;
        float returnTime = 0.15f;
        ApplyCooldown?.Invoke(0.5f + attackTime + returnTime);
        chargeStarted = -1;

        var seq = DOTween.Sequence();

        if (hasCharged)
        {
            hasCharged = false;
            seq.AppendCallback(() => StateChanged.Invoke(WeaponState.Attack));
            seq.Append(t.DOBlendableLocalMoveBy(new Vector3(1.6f, 0, 0), attackTime).SetEase(Ease.InOutExpo));
            seq.Join(t.DOBlendableLocalMoveBy(new Vector3(0, 1.5f, 0), attackTime / 2).SetLoops(2, LoopType.Yoyo));
            seq.Join(t.DOLocalRotate(new Vector3(0, 0, -200f), attackTime, RotateMode.LocalAxisAdd).SetEase(Ease.InOutExpo));
            seq.AppendInterval(0.1f);
            seq.AppendCallback(() => StateChanged.Invoke(WeaponState.ReturningToIdle));
        }

        seq.Append(t.DOLocalMove(new Vector3(0, 0, 0), returnTime));
        seq.Join(t.DOLocalRotate(new Vector3(0, 0, 0), returnTime));
        seq.AppendCallback(() => StateChanged.Invoke(WeaponState.Idle));
        seq.AppendCallback(() => OnAttackSequenceStateChanged.Invoke(false));
    }
}