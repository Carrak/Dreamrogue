using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public interface IPool<T> : IDisposable 
    where T : Component
{
    GameObject ParentObject { get; }

    T Dequeue(bool activate = true);
    void Enqueue(T item);
}

public abstract class Pool<T> : IPool<T>
    where T : Component
{
    public GameObject ParentObject { get; }

    private readonly Queue<T> _queue;

    private readonly string _name;
    private readonly int _step;

    protected readonly int _initialCapacity;

    public Pool(string objectName, int initialCapacity, int step)
    {
        ParentObject = new GameObject(objectName + "Pool");
        _queue = new Queue<T>();

        _name = objectName;
        _initialCapacity = initialCapacity;
        _step = step;
    }

    public T Dequeue(bool activate = true)
    {
        if (!_queue.TryDequeue(out T result))
        {
            CreateAndEnqueue(_step);
            result = _queue.Dequeue();
        }
        
        if (activate)
            result.gameObject.SetActive(true);

        if (result is IPoolable<IPool<T>> poolable)
            poolable.OnSpawned(this);

        return result;
    }

    public void Enqueue(T item)
    {
        item.gameObject.SetActive(false);
        if (item is IPoolable<IPool<T>> poolable)
            poolable.OnDespawned();

        _queue.Enqueue(item);
    }

    protected void CreateAndEnqueue(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            var obj = Create();
            obj.name = _name;
            obj.transform.SetParent(ParentObject.transform);
            Enqueue(obj.GetComponent<T>());
        }
    }

    protected abstract T Create();

    public void Dispose()
    {
        _queue.Clear();
        UnityEngine.Object.Destroy(ParentObject);
    }
}

