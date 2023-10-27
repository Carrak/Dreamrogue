public class LifetimeController
{
    private readonly LifetimeModel _model;

    public LifetimeController(LifetimeModel model)
    {
        _model = model;
    }

    public void StartLifetime()
    {
        _model.StartLifetime();
    }

    public void EndLifetime()
    {
        _model.EndLifetime();
    }
}