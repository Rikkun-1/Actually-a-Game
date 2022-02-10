public sealed class ExecuteLookOrderSystems : Feature
{
    public ExecuteLookOrderSystems(Contexts contexts)
    {
        Add(new DeleteOldLookOrdersWhenNewAddedSystem(contexts));

        Add(new ExecuteLookDirectionOrderSystem(contexts));
        Add(new ExecuteLootAtPositionOrderSystem(contexts));
        Add(new ExecuteLookAtEntityOrderSystem(contexts));
    }
}