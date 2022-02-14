public sealed class PathfindingSystems : Feature
{
    public PathfindingSystems(Contexts contexts)
    {
        Add(new WalkabilityMapSystems(contexts));
        
        Add(new RemovePathFromDestroyedSystem(contexts));
        //Add(new RecalculatePathSystem(contexts));
    }
}