using UnityEngine;

public class PlayerIdleState : IState
{
    private readonly IMovementModel _movementModel;
    private readonly IMovementController _movementController;
    private readonly IWeaponInputController _weaponController;
    private readonly ProximityItemController _itemPickupController;
    private readonly PlayerStateManager _stateManager;

    public PlayerIdleState(
        IMovementModel movementModel,
        IMovementController movementController,
        IWeaponInputController weaponController,
        PlayerStateManager stateManager, 
        ProximityItemController itemPickupController)
    {
        _movementModel = movementModel;
        _movementController = movementController;
        _weaponController = weaponController;
        _stateManager = stateManager;
        _itemPickupController = itemPickupController;
    }

    public void EnterState()
    {
        _movementModel.OnMove += OnMove;
    }

    public void ExitState()
    {
        _movementModel.OnMove -= OnMove;
    }

    public void Update()
    {
        _movementController.Update();
        _weaponController.Update();
        _itemPickupController.Update();
    }

    private void OnMove(Vector2 vector)
    {
        if (vector != Vector2.zero)
            _stateManager.ChangeState(_stateManager.Moving);
    }
}