using UnityEngine;

public class KeyboardMovementController : IMovementController
{
    readonly IMovementModel _model;

    public KeyboardMovementController(IMovementModel model)
    {
        _model = model;
    }

    public void Update()
    {
        float hor = 0;
        float ver = 0;

        if (Input.GetKey(KeyCode.D))
            hor = _model.MovementSpeed;
        else if (Input.GetKey(KeyCode.A))
            hor = -_model.MovementSpeed;

        if (Input.GetKey(KeyCode.W))
            ver = _model.MovementSpeed;
        else if (Input.GetKey(KeyCode.S))
            ver = -_model.MovementSpeed;

        var vector = new Vector3(hor * Time.deltaTime, ver * Time.deltaTime);

        _model.Move(vector);
    }
}
