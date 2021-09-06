public sealed class OrdersExecutionSystems : Feature
{
    public OrdersExecutionSystems(Contexts contexts)
    {
        Add(new ExecuteLookDirectionOrderSystem(contexts));
    }
}