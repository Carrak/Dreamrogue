using UnityEngine;
using Zenject;

public class PlayerWeaponView : MonoBehaviour
{
    private const float PROXIMITY_X = 0.6f;
    private const float PROXIMITY_Y = 0.8f;

    [SerializeField]
    private GameObject player;

    private IWeaponModel _weapon;

    [Inject]
    public void Inject(IWeaponModel weapon)
    {
        _weapon = weapon;
    }

    private void Start()
    {
        enabled = false;
        _weapon.OnWeaponEquipped += OnWeaponEquipped;
    }

    private void OnWeaponEquipped(IWeapon oldWeapon, IWeapon newWeapon)
    {
        if (newWeapon is null)
            enabled = false;
        else
            enabled = true;
    }

    private void Update()
    {
        if (_weapon.CanPoint)
            PointTo(Camera.main.ScreenToWorldPoint(Input.mousePosition));
    }

    private void OnDestroy()
    {
        _weapon.OnWeaponEquipped -= OnWeaponEquipped;
    }

    private void PointTo(Vector3 where)
    {
        Vector2 direction = where - player.transform.position;
        float angle = Vector2.SignedAngle(new Vector2(0, 1f), direction);

        float sin = Mathf.Sin(Mathf.Deg2Rad * angle);
        float cos = Mathf.Cos(Mathf.Deg2Rad * angle);

        float x = -sin * PROXIMITY_X;
        float y = cos * PROXIMITY_Y;

        var flip = angle > 0 ? -1 : 1;
        /*
        transform.localScale = new Vector3(flip, 1, 1);
        transform.localPosition = new Vector3(x, y + 0.1f, -1);
        transform.eulerAngles = new Vector3(0, 0, angle + 45 * flip);
        */

        transform.localScale = new Vector3(flip, 1, 1);
        transform.localPosition = new Vector3(x, y, 0);
        transform.eulerAngles = new Vector3(0, 0, angle);
    }
}
