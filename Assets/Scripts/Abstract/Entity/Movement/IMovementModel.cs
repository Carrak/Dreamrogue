using UnityEngine;

public delegate void OnMoveHandler(Vector2 vector);

public interface IMovementModel
{
    float MovementSpeed { get; }

    Vector2 MovementDirection { get; }

    event OnMoveHandler OnMove;

    void Move(Vector2 vector);
}