public sealed class MovementSystems : Feature
{
    public MovementSystems(Contexts contexts)
    {
        Add(new ProcessCalculateVelocityByPositionChangesRequestsSystem(contexts));

        Add(new UpdatePreviousPositionSystem(contexts));
        Add(new MoveSystem(contexts));

        Add(new TraversePathSystem(contexts));
        Add(new UpdateGridPositionRelyingOnWorldPositionSystem(contexts));
        
        Add(new CalculateVelocityByPositionChangesSystem(contexts));
        
        Add(new RotateTowardsMovementDirectionSystem(contexts));
    }
}