using System;
using UnityEngine;

public class ControllableProjectileMovement : IProjectileMovement
{
    private readonly Settings _settings;

    public ControllableProjectileMovement(Settings settings)
    {
        _settings = settings;
    }

    public void Update(Transform t)
    {
        
    }

    [Serializable]
    public class Settings : IProjectileMovementSettings
    {

    }
}