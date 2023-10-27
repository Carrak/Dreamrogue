public class ProjectilePool : FactoryPool<Projectile.Settings, 
    IProjectileMovementSettings, 
    IProjectileHitBehaviourSettings, 
    Projectile>
{
    public ProjectilePool(Projectile.Factory factory, 
        Projectile.Settings p1, 
        IProjectileMovementSettings p2, 
        IProjectileHitBehaviourSettings p3, 
        string objectName, 
        int initialCapacity, 
        int step) : 
        base(factory, p1, p2, p3, objectName, initialCapacity, step)
    { }
}