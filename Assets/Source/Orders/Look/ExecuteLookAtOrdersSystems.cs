public sealed class ExecuteLookAtOrdersSystems : Feature
{
    public ExecuteLookAtOrdersSystems(Contexts contexts)
    {
        Add(new DeleteOldLookOrdersWhenNewAddedSystem(contexts));

        Add(new ExecuteLookDirectionOrderSystem(contexts));
        Add(new ExecuteLootAtPositionOrderSystem(contexts));
        Add(new ExecuteLookAtEntityOrderSystem(contexts));
    }
}