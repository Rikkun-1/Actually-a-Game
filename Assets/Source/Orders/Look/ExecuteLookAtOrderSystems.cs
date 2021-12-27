public sealed class ExecuteLookAtOrderSystems : Feature
{
    public ExecuteLookAtOrderSystems(Contexts contexts)
    {
        Add(new DeleteOldLookOrdersWhenNewAddedSystem(contexts));

        Add(new ExecuteLookDirectionOrderSystem(contexts));
        Add(new ExecuteLootAtPositionOrderSystem(contexts));
        Add(new ExecuteLookAtEntityOrderSystem(contexts));
    }
}