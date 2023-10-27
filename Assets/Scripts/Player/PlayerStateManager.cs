using Zenject;

public class PlayerStateManager : StateMachine
{
    public PlayerIdleState Idle { get; private set; }
    public PlayerMovingState Moving { get; private set; }
    public PlayerDashingState Dashing { get; private set; }

    [Inject]
    public void Inject(
        PlayerIdleState idle,
        PlayerMovingState moving,
        PlayerDashingState dashing
        )
    {
        Idle = idle;
        Moving = moving;
        Dashing = dashing;

        initialState = idle;
    }
}