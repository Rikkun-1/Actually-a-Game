using UnityEngine;
using Entitas;

public sealed class PathfindingSystems : Feature
{

    public PathfindingSystems(Contexts contexts)
    {
        Add(new ResizePathfindingMapSystem(contexts));
        Add(new TestGridUpdatingSystem(contexts));
        Add(new UpdateNonWalkableMapSystem(contexts));
        Add(new DrawWalkableTilesSystem(contexts));
    }
}