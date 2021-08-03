using UnityEngine;
using Entitas;

public sealed class RootSystems : Feature 
{
    public RootSystems(Contexts contexts)
    {
        // Initialize
        Add(new CreateGridSystem(contexts));
        
        // Reactive
        Add(new AddViewSystem(contexts));
        Add(new ResizePathfindingMapSystem(contexts));
        Add(new UpdateNonWalkableMapSystem(contexts));
        
        // Execute
        Add(new DrawWalkableTilesSystem(contexts));
        Add(new TestSystem(contexts));

        // Cleanup
        Add(new DestroySystem(contexts));

        // Entitas generated
        Add(new GameEventSystems(contexts));
    }
}