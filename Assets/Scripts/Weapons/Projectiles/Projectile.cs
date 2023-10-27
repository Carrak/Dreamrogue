using System;
using UnityEngine;
using Zenject;

public class Projectile : MonoBehaviour, IPoolable<IPool<Projectile>>
{
    public bool CanHit { get; set; }

    private IProjectileMovement _movement;
    private IProjectileHitBehaviour _hitBehaviour;
    private LifetimeController _lifetimeController;
    private LifetimeModel _lifetimeModel;

    private IPool<Projectile> _pool;

    private SpriteRenderer _spriteRenderer;
    private BoxCollider2D _collider;

    private Settings _settings;

    [Inject]
    public void Inject(Settings settings,
        IProjectileMovement movement,
        IProjectileHitBehaviour hitBehaviour,
        LifetimeController ltController,
        LifetimeModel ltModel)
    {
        _settings = settings;
        _movement = movement;
        _hitBehaviour = hitBehaviour;
        _lifetimeController = ltController;
        _lifetimeModel = ltModel;
    }

    public void ReturnToQueue()
    {
        _pool.Enqueue(this);
    }

    public void OnSpawned(IPool<Projectile> p1)
    {
        _pool = p1;
        CanHit = true;
        _lifetimeController.StartLifetime();
    }

    public void OnDespawned()
    {
        _pool = null;
        _lifetimeController.EndLifetime();
    }

    private void LifetimeShouldEnd()
    {
        ReturnToQueue();
    }

    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _collider = GetComponent<BoxCollider2D>();

        _collider.offset = new Vector2(_settings.Hitbox.X, _settings.Hitbox.Y);
        _collider.size = new Vector2(_settings.Hitbox.W, _settings.Hitbox.H);
        _spriteRenderer.sprite = _settings.Sprite;

        _lifetimeModel.OnLifetimeShouldEnd += LifetimeShouldEnd;
    }

    private void OnDestroy()
    {
        _lifetimeModel.OnLifetimeShouldEnd -= LifetimeShouldEnd;
    }

    private void Update()
    {
        _movement.Update(transform);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!CanHit)
            return;

        if (collision.GetComponent<IDamageable>() is IDamageable enemy)
            _hitBehaviour.OnHit(this, enemy);
    }

    public class Factory : PlaceholderFactory<Settings,
        IProjectileMovementSettings,
        IProjectileHitBehaviourSettings, 
        Projectile> { }

    [Serializable]
    public class Settings
    {
        public DamageParamsRaw Damage;
        public bool IsPiercing;
        public float PiercingHitCooldown;
        public float Lifetime;
        public Sprite Sprite;
        public ColliderBorders Hitbox;
    }
}