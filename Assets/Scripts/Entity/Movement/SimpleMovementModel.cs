using System;
using UnityEngine;

public class SimpleMovementModel : IMovementModel
{
    public float MovementSpeed => _settings.MovementSpeed;

    public Vector2 MovementDirection { get; private set; }

    private readonly Settings _settings;

    public SimpleMovementModel(Settings settings)
    {
        _settings = settings;
    }

    public event OnMoveHandler OnMove;

    public void Move(Vector2 vector)
    {
        MovementDirection = vector.normalized;
        OnMove?.Invoke(vector);
    }

    [Serializable]
    public class Settings
    {
        public float MovementSpeed;
    }
}