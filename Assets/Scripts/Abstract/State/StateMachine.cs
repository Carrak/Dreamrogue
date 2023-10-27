using Zenject;

public abstract class StateMachine : IInitializable
{
    protected IState initialState;
    protected IState currentStateHandler;

    public void ChangeState(IState state)
    {
        if (currentStateHandler == state)
            return;

        currentStateHandler?.ExitState();
        currentStateHandler = state;
        currentStateHandler.EnterState();
    }

    public void Initialize()
    {
        ChangeState(initialState);
    }

    public void Update()
    {
        currentStateHandler.Update();
    }
}