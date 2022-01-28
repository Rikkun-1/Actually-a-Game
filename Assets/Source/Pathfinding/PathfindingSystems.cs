public sealed class PathfindingSystems : Feature
{
    public PathfindingSystems(Contexts contexts)
    {
        Add(new TestGridNonWalkableSystem(contexts));
        Add(new TestGridWallsSystem(contexts));
        
        Add(new UpdateGridPositionRelyingOnWorldPositionSystem(contexts));
        
        Add(new WalkabilityMapSystems(contexts));

        Add(new RecalculatePathSystem(contexts));
        Add(new ProcessPathRequestsSystem(contexts));
        Add(new TraversePathSystem(contexts));
    }
}