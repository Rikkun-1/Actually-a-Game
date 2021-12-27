public sealed class ExecuteMoveToOrderSystems : Feature
{
    public ExecuteMoveToOrderSystems(Contexts contexts)
    {
        Add(new ExecuteMoveToPositionOrderSystem(contexts));
    }
}