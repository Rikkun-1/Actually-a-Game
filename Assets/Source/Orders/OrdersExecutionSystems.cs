public sealed class OrdersExecutionSystems : Feature
{
    public OrdersExecutionSystems(Contexts contexts)
    {
        Add(new ExecuteLookAtOrderSystems(contexts));
        Add(new ExecuteShootAtOrderSystems(contexts));
        Add(new ExecuteMoveToOrderSystems(contexts));
    }
}