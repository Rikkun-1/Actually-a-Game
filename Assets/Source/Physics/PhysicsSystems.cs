public sealed class PhysicsSystems : Feature
{
    public PhysicsSystems(Contexts contexts)
    {
        Add(new SimulatePhysicsSystem(contexts));
        Add(new SyncTransformsSystem(contexts));
        Add(new ExecuteBulletRaycastHitCheck(contexts));
        Add(new DeleteBulletHitsSystem(contexts));
    }
}