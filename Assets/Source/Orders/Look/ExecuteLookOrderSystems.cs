public sealed class ExecuteLookOrderSystems : Feature
{
    public ExecuteLookOrderSystems(Contexts contexts)
    {
        Add(new ExecuteLookOrderSystem(contexts));
    }
}