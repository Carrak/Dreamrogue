using UnityEngine;
using Zenject;

public class PlayerView : MonoBehaviour
{
    private PlayerStateManager _stateManager;

    [Inject]
    public void Init(PlayerStateManager stateManager)
    {
        _stateManager = stateManager;
    }

    void Update()
    {
        _stateManager.Update();
    }
}
