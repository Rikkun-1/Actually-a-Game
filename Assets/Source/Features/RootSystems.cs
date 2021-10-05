public sealed class RootSystems : Feature
{
    public RootSystems(Contexts contexts)
    {
        Add(new GridSystems(contexts));

        Add(new MoveSystem(contexts));
        Add(new OrdersExecutionSystems(contexts));
        Add(new HealthAndDamageSystems(contexts));

        Add(new PathfindingSystems(contexts));
        Add(new ViewSystems(contexts));
        Add(new TargetVisibleSystem(contexts));
        Add(new TargetNotVisibleSystem(contexts));

        Add(new DeleteCollisionsSystem(contexts));
        Add(new DestructionSystems(contexts));

        Add(new GameEventSystems(contexts));
    }
}