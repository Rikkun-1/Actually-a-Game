public sealed class CollisionSystems : Feature
{
    public CollisionSystems(Contexts contexts)
    {
        Add(new DeleteCollisionSystem(contexts));
    }
}