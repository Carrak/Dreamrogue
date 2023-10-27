using UnityEngine;

public class PlayerMovingState : IState
{
    private readonly IMovementModel _movementModel;
    private readonly IMovementController _movementController;
    private readonly IWeaponInputController _weaponController;
    private readonly PlayerDashController _dashController;
    private readonly ProximityItemController _itemPickupController;

    private readonly PlayerDashModel _dashModel;

    private readonly PlayerStateManager _stateManager;
    private readonly PlayerView _view;

    public PlayerMovingState(IMovementModel movementModel,
        IMovementController movementController,
        IWeaponInputController weaponController,
        PlayerStateManager stateManager,
        PlayerView view,
        ProximityItemController itemPickupController, 
        PlayerDashController dashController, 
        PlayerDashModel dashModel)
    {
        _movementModel = movementModel;
        _movementController = movementController;
        _weaponController = weaponController;
        _stateManager = stateManager;
        _view = view;
        _itemPickupController = itemPickupController;
        _dashController = dashController;
        _dashModel = dashModel;
    }

    public void EnterState()
    {
        _movementModel.OnMove += OnMove;
        _dashModel.OnDash += OnDash;
    }

    public void ExitState()
    {
        _movementModel.OnMove -= OnMove;
        _dashModel.OnDash -= OnDash;
    }

    public void Update()
    {
        _movementController.Update();
        _weaponController.Update();
        _itemPickupController.Update();
        _dashController.Update();
    }

    private void OnMove(Vector2 vector)
    {
        if (vector == Vector2.zero)
            _stateManager.ChangeState(_stateManager.Idle);

        _view.transform.position += (Vector3)vector;
    }

    private void OnDash()
    {
        _stateManager.ChangeState(_stateManager.Dashing);
    }
}