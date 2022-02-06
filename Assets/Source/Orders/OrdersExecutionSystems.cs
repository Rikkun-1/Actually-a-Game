public sealed class OrdersExecutionSystems : Feature
{
    public OrdersExecutionSystems(Contexts contexts)
    {
        Add(new RemoveOrdersFromDestroyedSystems(contexts));

        Add(new ExecuteLookOrderSystems(contexts));
        Add(new ExecuteShootOrderSystems(contexts));
    }
}