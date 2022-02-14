public sealed class ExecuteMoveOrderSystems : Feature
{
    public ExecuteMoveOrderSystems(Contexts contexts)
    {
        Add(new ExecuteMoveToPositionOrderSystem(contexts));
        
        Add(new RemoveMoveToPositionOrderWhenTargetReachedSystem(contexts));
    }
}