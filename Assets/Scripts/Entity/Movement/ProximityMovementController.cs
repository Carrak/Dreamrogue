using UnityEngine;

public class ProximityMovementController : IMovementController
{
    private readonly IMovementModel _model;
    private readonly Transform _transform;

    public ProximityMovementController(IMovementModel model, Transform transform)
    {
        _model = model;
        _transform = transform;
    }

    public void Update()
    {
        Collider2D collider = Physics2D.OverlapCircle(_transform.position, 3, LayerMask.GetMask("Player"));

        if (collider is null)
        {
            _model.Move(Vector3.zero);
            return;
        }

        Vector2 dir = (collider.transform.position - _transform.position).normalized;
        _model.Move(_model.MovementSpeed * Time.deltaTime * dir);
    }
}