using UnityEngine;
using Zenject;

public class DebugSpawner : MonoBehaviour
{
    [Inject]
    SlimeView.Factory _slimeFactory;

    [Inject]
    PickableWeapon.Factory _weaponFactory;

    [Inject]
    BasicSword _sword;

    [Inject]
    CrimsonDagger _dagger;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightBracket)) 
        {
            var slime = _slimeFactory.Create();
            var pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            slime.transform.position = new Vector3(pos.x, pos.y, 0);
        }

        if (Input.GetKeyDown(KeyCode.LeftBracket))
        {
            var weapon = _weaponFactory.Create(_sword);
            var pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            weapon.transform.position = new Vector3(pos.x, pos.y, 0);
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            var weapon = _weaponFactory.Create(_dagger);
            var pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            weapon.transform.position = new Vector3(pos.x, pos.y, 0);
        }
    }
}