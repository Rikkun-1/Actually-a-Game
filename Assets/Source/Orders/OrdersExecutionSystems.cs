public sealed class OrdersExecutionSystems : Feature
{
    public OrdersExecutionSystems(Contexts contexts)
    {
        Add(new ExecuteLookAtOrdersSystems(contexts));
        Add(new ExecuteShootAtOrdersSystems(contexts));
    }
}