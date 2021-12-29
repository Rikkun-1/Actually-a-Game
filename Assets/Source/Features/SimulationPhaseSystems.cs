public sealed class SimulationPhaseSystems : Feature
{
    public SimulationPhaseSystems(Contexts contexts)
    {
        Add(new GridSystems(contexts));

        Add(new MovementSystems(contexts));
        Add(new OrdersExecutionSystems(contexts));
        Add(new HealthAndDamageSystems(contexts));

        Add(new PathfindingSystems(contexts));
        Add(new ViewSystems(contexts));
        Add(new TargetVisibilitySystems(contexts));

        Add(new CollisionSystems(contexts));
        Add(new DestructionSystems(contexts));

        Add(new GameEventSystems(contexts));
    }
}