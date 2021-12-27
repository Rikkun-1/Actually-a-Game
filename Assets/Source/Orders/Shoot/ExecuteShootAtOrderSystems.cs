public sealed class ExecuteShootAtOrderSystems : Feature
{
    public ExecuteShootAtOrderSystems(Contexts contexts)
    {
        Add(new DeleteOldShootOrdersWhenNewAddedSystem(contexts));
        Add(new ExecuteShootAtDirectionOrderSystem(contexts));
        Add(new ExecuteShootAtPositionOrderSystem(contexts));
        Add(new ExecuteShootAtEntityOrderSystem(contexts));
    }
}