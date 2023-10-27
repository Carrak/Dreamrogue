using UnityEngine;
using Zenject;

public class WeaponView : MonoBehaviour
{
    private IWeaponModel _model;
    private IWeaponInputController _controller;
    private PickableWeapon.Factory _itemFactory;
    private FlashEffect _flash;

    private BoxCollider2D _weaponCollider;
    private SpriteRenderer _weaponRenderer;

    private bool delayedReleaseNormal;
    private bool delayedReleaseSpecial;

    [Inject]
    public void Inject(IWeaponModel model, 
        IWeaponInputController controller, 
        FlashEffect flash, 
        PickableWeapon.Factory itemFactory)
    {
        _model = model;
        _controller = controller;
        _flash = flash;
        _itemFactory = itemFactory;
    }

    private void Start()
    {
        _weaponCollider = GetComponent<BoxCollider2D>();
        _weaponRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        _model.OnReleaseNormal -= DelayedReleaseNormal;
        _model.OnReleaseSpecial -= DelayedReleaseSpecial;

        if (delayedReleaseNormal)
        {
            _model.HeldWeapon.ReleaseNormal(transform);
            delayedReleaseNormal = false;
        }
        else if (delayedReleaseSpecial)
        {
            _model.HeldWeapon.ReleaseSpecial(transform);
            delayedReleaseSpecial = false;
        }

        _model.OnWeaponEquipped += OnWeaponEquipped;
        _model.OnPressNormal += PressNormal;
        _model.OnHoldNormal += HoldNormal;
        _model.OnReleaseNormal += ReleaseNormal;
        _model.OnPressSpecial += PressSpecial;
        _model.OnHoldSpecial += HoldSpecial;
        _model.OnReleaseSpecial += ReleaseSpecial;

        _model.OnStateChanged += OnStateChanged;
    }

    private void OnDisable()
    {
        _model.OnWeaponEquipped -= OnWeaponEquipped;
        _model.OnPressNormal -= PressNormal;
        _model.OnHoldNormal -= HoldNormal;
        _model.OnReleaseNormal -= ReleaseNormal;
        _model.OnPressSpecial -= PressSpecial;
        _model.OnHoldSpecial -= HoldSpecial;
        _model.OnReleaseSpecial -= ReleaseSpecial;

        _model.OnStateChanged -= OnStateChanged;

        _model.OnReleaseNormal += DelayedReleaseNormal;
        _model.OnReleaseSpecial += DelayedReleaseSpecial;
    }

    private void OnDestroy()
    {
        _model.OnReleaseNormal -= DelayedReleaseNormal;
        _model.OnReleaseSpecial -= DelayedReleaseSpecial;
    }

    private void OnStateChanged(WeaponState state)
    {
        if (state == WeaponState.Charged)
        {
            _flash.DoFlash();
            return;
        }
    }

    private void PressNormal() => _model.HeldWeapon.PressNormal(transform);
    private void HoldNormal() => _model.HeldWeapon.HoldNormal(transform);
    private void ReleaseNormal() => _model.HeldWeapon.ReleaseNormal(transform);
    private void PressSpecial() => _model.HeldWeapon.PressSpecial(transform);
    private void HoldSpecial() => _model.HeldWeapon.HoldSpecial(transform);
    private void ReleaseSpecial() => _model.HeldWeapon.ReleaseSpecial(transform);

    private void OnWeaponEquipped(IWeapon oldWeapon, IWeapon newWeapon)
    {
        bool enableCollider = false;
        if (newWeapon is IMelee sword)
        {
            enableCollider = true;
            _weaponCollider.offset = new Vector2(sword.Hitbox.X, sword.Hitbox.Y);
            _weaponCollider.size = new Vector2(sword.Hitbox.W, sword.Hitbox.H);
        }

        _weaponCollider.enabled = enableCollider;
        _weaponRenderer.sprite = newWeapon.Sprite;

        if (oldWeapon != null) 
        {
            var oldPickable = _itemFactory.Create(oldWeapon);
            oldPickable.transform.position = transform.position;
        }
    }

    private void DelayedReleaseNormal() => delayedReleaseNormal = true;
    private void DelayedReleaseSpecial() => delayedReleaseSpecial = true;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.GetComponent<IDamageable>() is IDamageable enemy)
            _controller.HandleHit(enemy, transform.parent.up);
    }
}
