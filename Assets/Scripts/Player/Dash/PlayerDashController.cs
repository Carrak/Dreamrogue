using UnityEngine;

public class PlayerDashController
{
    private readonly PlayerDashModel _model;

    public PlayerDashController(PlayerDashModel model)
    {
        _model = model;
    }

    public void Update()
    {
        if (Input.GetKey(KeyCode.Space))
            _model.Dash();
    }
}