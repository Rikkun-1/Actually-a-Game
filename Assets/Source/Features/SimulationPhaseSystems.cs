public sealed class SimulationPhaseSystems : Feature
{
    public SimulationPhaseSystems(Contexts contexts)
    {
        Add(new GridSystems(contexts));

        Add(new MovementSystems(contexts));
        Add(new HealthAndDamageSystems(contexts));

        Add(new PathfindingSystems(contexts));
        Add(new ImmediateAISystems(contexts));
        Add(new ViewSystems(contexts));

        Add(new CollisionSystems(contexts));
        Add(new DestructionSystems(contexts));
    }
}

public sealed class ImmediateAISystems : Feature
{
    public ImmediateAISystems(Contexts contexts)
    {
        Add(new OrdersExecutionSystems(contexts));
        Add(new ShootAtEnemiesInSightSystems(contexts));
    }
}