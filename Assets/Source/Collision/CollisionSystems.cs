public sealed class CollisionSystems : Feature
{
    public CollisionSystems(Contexts contexts)
    {
        Add(new ExecuteBulletRaycastHitCheck(contexts));
        Add(new DeleteBulletHitsSystem(contexts));
    }
}