public sealed class OrdersExecutionSystems : Feature
{
    public OrdersExecutionSystems(Contexts contexts)
    {
        Add(new ExecuteLookOrderSystems(contexts));
        Add(new ExecuteShootOrderSystems(contexts));
        Add(new ExecuteMoveOrderSystems(contexts));
    }
}