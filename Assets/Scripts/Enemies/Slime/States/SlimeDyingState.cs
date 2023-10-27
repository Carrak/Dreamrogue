using DG.Tweening;
using UnityEngine;

public class SlimeDyingState : IState
{
    private readonly Animator _animator;
    private readonly SpriteRenderer _sprite;
    private readonly SlimeView _view;

    public SlimeDyingState(Animator animator, SpriteRenderer sprite, SlimeView view)
    {
        _animator = animator;
        _sprite = sprite;
        _view = view;
    }

    public void EnterState()
    {
        _animator.Play("Death");

        var seq = DOTween.Sequence();

        seq.AppendInterval(0.5f);
        seq.Append(_sprite.DOFade(0, 0.5f));
        seq.AppendCallback(() => Object.Destroy(_view.gameObject));

        seq.Play();
    }

    public void ExitState()
    {
        
    }

    public void Update()
    {
        
    }
}