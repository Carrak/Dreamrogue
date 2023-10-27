using System;

public delegate void OnDashHandler();

public class PlayerDashModel
{
    public int Afterimages => _settings.Afterimages;
    public float AfterimageDuration => _settings.AfterimageDuration;
    public float Duration => _settings.Duration;
    public float Distance => _settings.Distance;

    public event OnDashHandler OnDash;

    private readonly IMovementModel _movement;
    private readonly Settings _settings;

    public PlayerDashModel(IMovementModel movement, Settings settings)
    {
        _movement = movement;
        _settings = settings;
    }

    public void Dash()
    {
        OnDash?.Invoke();
    }

    [Serializable]
    public class Settings
    {
        public int Afterimages;
        public float AfterimageDuration;
        public float Duration;
        public float Distance;
    }
}