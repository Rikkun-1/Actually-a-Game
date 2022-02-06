public sealed class ImmediateAISystems : Feature
{
    public ImmediateAISystems(Contexts contexts)
    {
        Add(new OrdersExecutionSystems(contexts));
        Add(new ShootAtEnemiesInSightSystems(contexts));
    }
}