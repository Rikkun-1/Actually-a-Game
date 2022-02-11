public sealed class SimulationPhaseSystems : Feature
{
    public SimulationPhaseSystems(Contexts contexts)
    {
        Add(new GameEventSystems(contexts));
        Add(new GridSystems(contexts));

        Add(new MovementSystems(contexts));
        Add(new PhysicsSystems(contexts));
        Add(new HealthAndDamageSystems(contexts));

        Add(new PathfindingSystems(contexts));
        Add(new ImmediateAISystems(contexts));
        Add(new ViewSystems(contexts));

        Add(new DestructionSystems(contexts));
    }
}