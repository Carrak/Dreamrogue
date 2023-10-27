using UnityEngine;

public class SlimeAliveState : IState
{
    private readonly IDamageableModel _damageableModel;
    private readonly IMovementController _movementController;
    private readonly IMovementModel _movement;
    private readonly SlimeStateManager _stateManager;
    private readonly SlimeView _view;

    public SlimeAliveState(
        IDamageableModel model, 
        IMovementController controller, 
        SlimeStateManager stateManager,
        IMovementModel movement,
        SlimeView view)
    {
        _damageableModel = model;
        _movementController = controller;
        _stateManager = stateManager;
        _movement = movement;
        _view = view;
    }

    public void EnterState()
    {
        _damageableModel.OnDeath += OnDeath;
        _movement.OnMove += OnMove;
    }

    public void ExitState()
    {
        _damageableModel.OnDeath -= OnDeath;
        _movement.OnMove -= OnMove;
    }

    private void OnMove(Vector2 vector)
    {
        _view.transform.position += (Vector3)vector;
    }

    public void Update()
    {
        _movementController.Update();
    }

    private void OnDeath()
    {
        _stateManager.ChangeState(SlimeState.Dying);
    }
}