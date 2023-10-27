using System.Collections.Generic;
using UnityEngine;

public class ProjectilePierceTracker
{
    private readonly float _hitCd;
    private readonly Dictionary<int, float> _hits = new();

    private readonly LifetimeModel _lifetime;

    public ProjectilePierceTracker(float hitCooldown, LifetimeModel lifetime)
    {
        _hitCd = hitCooldown;
        _lifetime = lifetime;

        _lifetime.OnLifetimeEnd += ClearHits;
    }

    ~ProjectilePierceTracker()
    {
        _lifetime.OnLifetimeEnd -= ClearHits;
    }

    public bool CanHitEnemy(IDamageable enemy) => !_hits.TryGetValue(enemy.Id, out var time) || Time.time > time;

    public void AddHitEnemy(IDamageable enemy) => _hits[enemy.Id] = Time.time + _hitCd;

    public void ClearHits() => _hits.Clear();
}
