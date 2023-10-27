using UnityEngine;

public class PrefabPool<T> : Pool<T>
    where T : Component
{
    private readonly GameObject _prefab;

    public PrefabPool(GameObject prefab, string objectName, int initialCapacity, int step)
        : base(objectName, initialCapacity, step)
    {
        _prefab = prefab;
        CreateAndEnqueue(_initialCapacity);
    }

    protected override T Create() => Object.Instantiate(_prefab).GetComponent<T>();
}