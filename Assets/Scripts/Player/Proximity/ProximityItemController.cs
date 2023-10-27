using UnityEngine;

public class ProximityItemController
{
    private readonly IWeaponModel _weaponModel;
    private readonly ProximityItemModel _model;
    private readonly Transform _transform;

    public ProximityItemController(IWeaponModel weaponModel, ProximityItemModel model, Transform transform)
    {
        _weaponModel = weaponModel;
        _model = model;
        _transform = transform;
    }

    public void Update()
    {
        CheckForItems();
        CheckInput();
    }

    private void CheckForItems()
    {
        var colliders = Physics2D.OverlapCircleAll(_transform.position, 0.5f, 1);

        foreach (var col in colliders)
            if (col.GetComponent<PickableWeapon>() is PickableWeapon pickableWeapon)
            {
                if (_model.HighlightedPickableWeapon != null)
                    _model.HighlightedPickableWeapon.StopHighlight();

                pickableWeapon.DoHighlight();
                _model.ChangeWeapon(pickableWeapon);
                return;
            }

        if (_model.HighlightedPickableWeapon != null)
        {
            _model.HighlightedPickableWeapon.StopHighlight();
            _model.ChangeWeapon(null);
        }
    }

    private void CheckInput()
    {
        if (_model.HighlightedPickableWeapon != null && Input.GetKeyDown(KeyCode.E))
        {
            _weaponModel.EquipWeapon(_model.HighlightedPickableWeapon.Weapon);
            _model.HighlightedPickableWeapon.Destroy();
        }    
    }
}