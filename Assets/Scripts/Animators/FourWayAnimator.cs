using System;
using UnityEngine;
using Zenject;

public class FourWayAnimator : MonoBehaviour
{
    private string currentAnimation = "IdleDown";
    private Direction currentDirection = Direction.Down;

    private Animator animator;

    private IMovementModel _model;

    [Inject]
    public void Init(IMovementModel pm)
    {
        _model = pm;
    }

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        _model.OnMove += ChangeAnimation;
    }

    private void OnDisable()
    {
        _model.OnMove -= ChangeAnimation;
    }

    private void ChangeAnimation(Vector2 vector)
    {
        string a;

        if (vector == Vector2.zero)
        {
            a = "Idle" + currentDirection;
            ChangeAnimation(currentDirection, a);
            return;
        }

        Direction dir = Direction.Down;
        float absx = Mathf.Abs(vector.x);
        float absy = Mathf.Abs(vector.y);

        if (absx == absy)
        {
            var ver = GetVerticalDirection(vector.y);
            var hor = GetHorizontalDirection(vector.x);
            int verDiff = Math.Abs(currentDirection - GetVerticalDirection(vector.y));
            int horDiff = Math.Abs(currentDirection - GetHorizontalDirection(vector.x));
            dir = verDiff < horDiff ? ver : hor;
        }
        else if (absy > absx)
            dir = GetVerticalDirection(vector.y);
        else if (absx > absy)
            dir = GetHorizontalDirection(vector.x);

        // makes a string like "IdleLeft" to determine what animation to play
        a = "Walk" + dir;

        ChangeAnimation(dir, a);
    }

    private void ChangeAnimation(Direction dir, string a)
    {
        if (currentAnimation == a)
            return;

        currentDirection = dir;
        currentAnimation = a;

        animator.Play(a);
    }

    private Direction GetVerticalDirection(float y)
    {
        if (y > 0)
            return Direction.Up;
        else if (y < 0)
            return Direction.Down;

        throw new ArgumentException("y can't be 0");
    }

    private Direction GetHorizontalDirection(float x)
    {
        if (x > 0)
            return Direction.Right;
        else if (x < 0)
            return Direction.Left;

        throw new ArgumentException("x can't be 0");
    }
}
