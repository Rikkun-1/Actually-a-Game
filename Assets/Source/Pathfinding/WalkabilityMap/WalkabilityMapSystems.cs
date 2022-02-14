public sealed class WalkabilityMapSystems : Feature
{
    public WalkabilityMapSystems(Contexts contexts)
    {
        Add(new WalkabilityMapTestingSystems(contexts));

        Add(new ResizePathfindingMapSystem(contexts));
        Add(new RemoveWalkabilityMapComponentsOnEntityDestroyedSystem(contexts));
        Add(new UpdateNonWalkableMapSystem(contexts));
        Add(new UpdateWallMapSystem(contexts));
    }
}