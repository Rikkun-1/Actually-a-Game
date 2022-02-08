public sealed class ImmediateOrderExecutionSystems : Feature
{
    public ImmediateOrderExecutionSystems(Contexts contexts)
    {
        Add(new AddProperLookOrderToShootOrderSystem(contexts));
        
        Add(new RemoveReactionStartTimeWhenShootAtEntityOrderRemovedSystem(contexts));
        
        Add(new ExecuteMoveOrderSystems(contexts));
        
        Add(new ProcessPathRequestsSystem(contexts));
    }
}