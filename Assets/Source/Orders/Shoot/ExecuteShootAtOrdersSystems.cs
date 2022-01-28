public sealed class ExecuteShootAtOrdersSystems : Feature
{
    public ExecuteShootAtOrdersSystems(Contexts contexts)
    {
        Add(new DeleteOldShootOrdersWhenNewAddedSystem(contexts));  
        Add(new ExecuteShootAtDirectionOrderSystem(contexts));
        Add(new ExecuteShootAtPositionOrderSystem(contexts));
        Add(new ExecuteShootAtEntityOrderSystem(contexts));
    }
}