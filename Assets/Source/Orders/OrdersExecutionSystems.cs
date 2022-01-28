public sealed class OrdersExecutionSystems : Feature
{
    public OrdersExecutionSystems(Contexts contexts)
    {
        Add(new DeleteOldLookOrdersWhenNewAdded(contexts));
        
        Add(new ExecuteLookDirectionOrderSystem(contexts));
        Add(new ExecuteLootAtPositionOrderSystem(contexts));
        Add(new ExecuteLookAtEntityOrderSystem(contexts));
    }
}