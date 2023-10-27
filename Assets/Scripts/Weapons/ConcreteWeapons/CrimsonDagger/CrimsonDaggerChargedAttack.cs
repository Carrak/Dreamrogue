using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;

public class CrimsonDaggerChargedAttack : IAttackSequence
{
    public event OnWeaponStateChangedHandler StateChanged;
    public event OnApplyCooldownHandler ApplyCooldown;
    public event OnAttackSequenceStateChangedHandler OnAttackSequenceStateChanged;

    private readonly ProjectilePool _pool;

    private List<Projectile> _projectiles = new();
    private bool hasCharged = false;
    private bool held = false;

    public CrimsonDaggerChargedAttack(ProjectilePool pool)
    {
        _pool = pool;
    }

    public void Hold(Transform t)
    {
        _pool.ParentObject.transform.SetPositionAndRotation(t.position, t.rotation);

        if (!held)
            return;

        var target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        foreach (var p in _projectiles)
        {
            var where = p.transform.position - target;
            var angle = Vector2.SignedAngle(new Vector2(0, -1f), where);
            p.transform.eulerAngles = new Vector3(0, 0, angle);
        }
    }

    public void Press(Transform t)
    {
        held = true;
        OnAttackSequenceStateChanged.Invoke(true);
        StateChanged.Invoke(WeaponState.Charging);

        Projectile[] p = new Projectile[4];

        for (int i = 0; i < p.Length; i++)
        {
            p[i] = _pool.Dequeue(false);
            p[i].CanHit = false;
            p[i].transform.SetPositionAndRotation(t.position, t.rotation);
        }

        _projectiles = new List<Projectile>(p);

        var seq = DOTween.Sequence(t);

        seq.Append(t.DOBlendableLocalMoveBy(new Vector3(0, -2, 0), 1f));
        seq.AppendCallback(() =>
        {
            for (int i = 0; i < p.Length; i++)
            {
                p[i].transform.SetPositionAndRotation(t.position, t.rotation);
                p[i].gameObject.SetActive(true);
            }
        });

        var unfoldTime = 0.3f;
        var forwardBy = 0.15f;
        var sideBy = 0.5f;
        var ease = Ease.OutSine;

        seq.Append(p[0].transform.DOBlendableLocalMoveBy(new Vector3(sideBy, forwardBy, 0), unfoldTime).SetEase(ease));
        seq.Join(p[1].transform.DOBlendableLocalMoveBy(new Vector3(sideBy, forwardBy, 0), unfoldTime).SetEase(ease));
        seq.Join(p[2].transform.DOBlendableLocalMoveBy(new Vector3(-sideBy, forwardBy, 0), unfoldTime).SetEase(ease));
        seq.Join(p[3].transform.DOBlendableLocalMoveBy(new Vector3(-sideBy, forwardBy, 0), unfoldTime).SetEase(ease));

        seq.Append(p[1].transform.DOBlendableLocalMoveBy(new Vector3(sideBy, forwardBy, 0), unfoldTime).SetEase(ease));
        seq.Join(p[3].transform.DOBlendableLocalMoveBy(new Vector3(-sideBy, forwardBy, 0), unfoldTime).SetEase(ease));

        seq.AppendCallback(() => hasCharged = true);
        seq.AppendCallback(() => StateChanged.Invoke(WeaponState.Charged));
    }

    public void Release(Transform t)
    {
        if (!hasCharged)
        {
            Debug.Log("entered uncharged block");
            OnAttackSequenceStateChanged.Invoke(false);

            DOTween.Kill(t);
            StateChanged.Invoke(WeaponState.ReturningToIdle);
            t.DOLocalMove(new Vector3(0, 0, 0), 0.5f);

            StateChanged.Invoke(WeaponState.Idle);
            foreach (var p in _projectiles)
                _pool.Enqueue(p);

            _projectiles.Clear();
            return;
        }

        hasCharged = false;
        held = false;
        StateChanged.Invoke(WeaponState.Attack);
        foreach (var p in _projectiles)
            p.CanHit = true;

        var mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        var target = new Vector3(mouse.x, mouse.y);

        var mainSeq = DOTween.Sequence();

        // fast slash forward
        var attackDuration = 0.3f;
        var forwardBy = 1;
        var attackEase = Ease.OutExpo;
        var attackSeq = DOTween.Sequence();
        attackSeq.Append(t.DOMove(target + forwardBy * t.up, attackDuration).SetEase(attackEase));
        foreach (var p in _projectiles)
            attackSeq.Join(p.transform.DOMove(target + forwardBy * p.transform.up, attackDuration).SetEase(attackEase));
        
        mainSeq.Append(attackSeq);

        // projectiles folding
        var foldEase = Ease.InExpo;
        var foldDur = 0.2f;
        var foldSeq = DOTween.Sequence();
        foldSeq.Append(t.DOMove(target + forwardBy * t.up, foldDur));
        foreach (var p in _projectiles)
        {
            foldSeq.Join(p.transform.DOMove(target + forwardBy * t.up, foldDur).SetEase(foldEase));
            foldSeq.Join(p.transform.DORotate(t.rotation.eulerAngles, foldDur).SetEase(foldEase));
        }
        foldSeq.AppendCallback(() => _projectiles.ForEach((p) => _pool.Enqueue(p)));
        foldSeq.AppendCallback(() => _projectiles.Clear());
        mainSeq.Append(foldSeq);

        // returning
        mainSeq.AppendCallback(() => StateChanged.Invoke(WeaponState.ReturningToIdle));
        mainSeq.Append(t.DOLocalMove(new Vector3(0, 0, 0), 0.4f).SetEase(Ease.InQuart));
        mainSeq.AppendCallback(() => StateChanged.Invoke(WeaponState.Idle));
        mainSeq.AppendCallback(() => OnAttackSequenceStateChanged.Invoke(false));
    }
}
