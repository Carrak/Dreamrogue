using UnityEngine;
using Zenject;

public class FactoryPool<T> : Pool<T>
    where T : Component
{
    private readonly PlaceholderFactory<T> _factory;

    public FactoryPool(PlaceholderFactory<T> factory, 
        string objectName, 
        int initialCapacity, 
        int step) : 
        base(objectName, initialCapacity, step)
    {
        _factory = factory;
    }

    protected override T Create()
    {
        return _factory.Create();
    }
}

public class FactoryPool<TParam1, TValue> : Pool<TValue>
    where TValue : Component
{
    private readonly PlaceholderFactory<TParam1, TValue> _factory;
    private readonly TParam1 _p1;

    public FactoryPool(PlaceholderFactory<TParam1, TValue> factory,
        TParam1 p1,
        string objectName, 
        int initialCapacity, 
        int step) :
        base(objectName, initialCapacity, step)
    {
        _factory = factory;
        _p1 = p1;
    }

    protected override TValue Create()
    {
        return _factory.Create(_p1);
    }
}

public class FactoryPool<TParam1, TParam2, TValue> : Pool<TValue>
    where TValue : Component
{
    private readonly PlaceholderFactory<TParam1, TParam2, TValue> _factory;
    private readonly TParam1 _p1;
    private readonly TParam2 _p2;

    public FactoryPool(PlaceholderFactory<TParam1, TParam2, TValue> factory,
        TParam1 p1,
        TParam2 p2,
        string objectName, 
        int initialCapacity, 
        int step) :
        base(objectName, initialCapacity, step)
    {
        _factory = factory;
        _p1 = p1;
        _p2 = p2;
    }

    protected override TValue Create()
    {
        return _factory.Create(_p1, _p2);
    }
}

public class FactoryPool<TParam1, TParam2, TParam3, TValue> : Pool<TValue>
    where TValue : Component
{
    private readonly PlaceholderFactory<TParam1, TParam2, TParam3, TValue> _factory;
    private readonly TParam1 _p1;
    private readonly TParam2 _p2;
    private readonly TParam3 _p3;

    public FactoryPool(PlaceholderFactory<TParam1, TParam2, TParam3, TValue> factory,
        TParam1 p1,
        TParam2 p2,
        TParam3 p3,
        string objectName,
        int initialCapacity,
        int step) :
        base(objectName, initialCapacity, step)
    {
        _factory = factory;
        _p1 = p1;
        _p2 = p2;
        _p3 = p3;
    }

    protected override TValue Create()
    {
        return _factory.Create(_p1, _p2, _p3);
    }
}