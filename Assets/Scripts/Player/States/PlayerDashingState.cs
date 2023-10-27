using DG.Tweening;
using UnityEngine;

public class PlayerDashingState : IState
{
    private readonly IMovementModel _movement;
    private readonly PlayerStateManager _stateManager;
    private readonly PlayerDashModel _dash;
    private readonly IWeaponInputController _weaponController;

    private readonly SpriteRenderer _sr;
    private readonly PlayerView _view;
    private readonly WeaponView _weapon;

    private readonly IPool<SpriteRenderer> _queue;

    public PlayerDashingState(IMovementModel movement,
        PlayerStateManager stateManager,
        PlayerDashModel dash,
        SpriteRenderer sr,
        PlayerView view,
        WeaponView weapon, 
        IWeaponInputController weaponController,
        IPool<SpriteRenderer> dashQueue)
    {
        _movement = movement;
        _stateManager = stateManager;
        _dash = dash;
        _sr = sr;
        _view = view;
        _weapon = weapon;
        _weaponController = weaponController;
        _queue = dashQueue;
    }

    public void EnterState()
    {
        _weapon.gameObject.SetActive(false);

        var position = _view.transform.position;
        var destination = new Vector3(
            position.x + _movement.MovementDirection.x * _dash.Distance,
            position.y + _movement.MovementDirection.y * _dash.Distance,
            position.z);

        var sequence = DOTween.Sequence();
        sequence.Append(_view.transform.DOMove(destination, _dash.Duration).SetEase(Ease.OutSine));

        var trailSequence = DOTween.Sequence();
        for (int i = 0; i < _dash.Afterimages; i++)
        {
            trailSequence.AppendCallback(() => DoTrail());
            trailSequence.AppendInterval(_dash.Duration / _dash.Afterimages);
        }

        sequence.AppendCallback(() => _stateManager.ChangeState(_stateManager.Moving));
    }

    private void DoTrail()
    {
        var sequence = DOTween.Sequence();
        var sr = _queue.Dequeue().GetComponent<SpriteRenderer>();

        sequence.AppendCallback(() => {
            sr.sprite = _sr.sprite;
            sr.color = Color.white;
            sr.gameObject.transform.position = _view.transform.position;
        });

        sequence.Append(sr.DOFade(0, _dash.AfterimageDuration));
        sequence.AppendCallback(() => _queue.Enqueue(sr));
    }

    public void ExitState()
    {
        _weapon.gameObject.SetActive(true);
    }

    public void Update()
    {
        _weaponController.Update();
    }
}