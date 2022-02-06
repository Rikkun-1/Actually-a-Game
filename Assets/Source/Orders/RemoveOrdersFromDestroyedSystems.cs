public sealed class RemoveOrdersFromDestroyedSystems : Feature
{
    public RemoveOrdersFromDestroyedSystems(Contexts contexts)
    {
        Add(new RemoveMoveOrdersFromDestroyedSystem(contexts));
        Add(new RemoveLookOrdersWhenDestroyedSystem(contexts));
        Add(new RemoveShootOrdersFromDestroyedSystem(contexts));
    }
}