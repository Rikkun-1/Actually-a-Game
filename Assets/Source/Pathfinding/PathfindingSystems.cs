public sealed class PathfindingSystems : Feature
{
    public PathfindingSystems(Contexts contexts)
    {
        Add(new ResizePathfindingMapSystem(contexts));
        Add(new DrawWalkableTilesSystem(contexts));
        Add(new TestSystems(contexts));
        Add(new ProcessPathRequestsSystem(contexts));
        Add(new TraversePathSystem(contexts));
        Add(new DrawPathsSystem(contexts));
        Add(new DeletePathFindingComponentsOnEntityDestroyedSystem(contexts));
        Add(new UpdateNonWalkableMapSystem(contexts));
    }
}