public sealed class HealthAndDamageSystems : Feature
{
    public HealthAndDamageSystems(Contexts contexts)
    {
        Add(new DamageEntitiesByBulletSystem(contexts));
        Add(new ApplyDamageToHealthSystem(contexts));
        Add(new DestroyEntityOnZeroHealthSystem(contexts));
        Add(new RemoveDamageSystem(contexts));
    }
}