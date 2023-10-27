using Zenject;

public class SlimeStateManager : IInitializable
{
    private SlimeState currentState = SlimeState.None;
    private IState currentStateHandler;

    private IState[] _states;

    [Inject]
    public void Inject(
        SlimeAliveState alive,
        SlimeDyingState dying
        )
    {
        _states = new IState[] { alive, dying };
    }

    public void ChangeState(SlimeState state)
    {
        if (currentState == state)
            return;

        currentState = state;

        currentStateHandler?.ExitState();
        currentStateHandler = _states[(int)currentState];
        currentStateHandler.EnterState();
    }

    public void Initialize()
    {
        ChangeState(SlimeState.Alive);
    }

    public void Update()
    {
        currentStateHandler.Update();
    }
}

public enum SlimeState
{
    Alive,
    Dying,
    None
}