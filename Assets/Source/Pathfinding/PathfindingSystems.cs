public sealed class PathfindingSystems : Feature
{
    public PathfindingSystems(Contexts contexts)
    {
        Add(new ResizePathfindingMapSystem(contexts));
        
        Add(new TestGridNonWalkableSystem(contexts));
        Add(new TestGridWallsSystem(contexts));
        
        Add(new DeletePathFindingComponentsOnEntityDestroyedSystem(contexts));
        Add(new UpdateNonWalkableMapSystem(contexts));
        
        Add(new RecalculatePathSystem(contexts));
        Add(new ProcessPathRequestsSystem(contexts));
        Add(new TraversePathSystem(contexts));
    }
}