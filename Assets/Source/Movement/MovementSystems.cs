public sealed class MovementSystems : Feature
{
    public MovementSystems(Contexts contexts)
    {
        Add(new MoveSystem(contexts));
        Add(new TraversePathSystem(contexts));
    }
}