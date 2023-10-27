using DG.Tweening;
using System;

public delegate void OnLifetimeStartHandler();
public delegate void OnLifetimeShouldEndHandler();
public delegate void OnLifetimeEndHandler();

public class LifetimeModel
{
    public float Lifetime { get; }

    private static Random _rand = new();
    private int id;

    public event OnLifetimeStartHandler OnLifetimeStart;
    public event OnLifetimeEndHandler OnLifetimeEnd;
    public event OnLifetimeShouldEndHandler OnLifetimeShouldEnd;

    public LifetimeModel(float lifetime)
    {
        Lifetime = lifetime;
    }

    public void StartLifetime()
    {
        OnLifetimeStart?.Invoke();
        id = _rand.Next();

        if (Lifetime <= 0)
            return;

        var seq = DOTween.Sequence($"lifetime-{id}");
        seq.AppendInterval(Lifetime);
        seq.AppendCallback(() => OnLifetimeShouldEnd?.Invoke());
    }

    public void EndLifetime()
    {
        if (Lifetime > 0)
            DOTween.Kill($"lifetime-{id}");

        OnLifetimeEnd?.Invoke();
    }
}