public sealed class MovementSystems : Feature
{
    public MovementSystems(Contexts contexts)
    {
        Add(new ProcessCalculateVelocityByPositionChangesRequestsSystem(contexts));

        Add(new MoveSystem(contexts));
        Add(new CalculateVelocityByPositionChangesSystem(contexts));

        Add(new TraversePathSystem(contexts));
        Add(new UpdateGridPositionRelyingOnWorldPositionSystem(contexts));
    }
}