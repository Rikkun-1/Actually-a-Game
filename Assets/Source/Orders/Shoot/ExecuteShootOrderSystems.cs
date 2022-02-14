public sealed class ExecuteShootOrderSystems : Feature
{
    public ExecuteShootOrderSystems(Contexts contexts)
    {
        Add(new ExecuteShootOrderSystem(contexts));
    }
}