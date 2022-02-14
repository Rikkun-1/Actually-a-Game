public sealed class DestructionSystems : Feature
{
    public DestructionSystems(Contexts contexts)
    {
        Add(new RemoveDestroyedForIndestructibleSystem(contexts));
        Add(new DeletionSystem(contexts));
    }
}