using UnityEngine;
using Zenject;

public class OneWayAnimator : MonoBehaviour
{
    private string currentAnimation = "Idle";

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
        string a = vector != Vector2.zero ? "Walk" : "Idle";

        if (currentAnimation == a)
            return;

        currentAnimation = a;
        animator.Play(a);
    }
}