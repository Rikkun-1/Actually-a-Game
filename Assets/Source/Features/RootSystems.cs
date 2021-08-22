public sealed class RootSystems : Feature
{
    public RootSystems(Contexts contexts)
    {
        Add(new RemoveDestroyedForIndestructibleSystem(contexts));
        
        Add(new UpdateGridSizeSystem(contexts));
        
        Add(new PathfindingSystems(contexts));
        Add(new ViewSystems(contexts));

        Add(new DestroySystem(contexts));

        Add(new GameEventSystems(contexts));
    }
}