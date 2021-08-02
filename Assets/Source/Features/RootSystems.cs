using UnityEngine;
using Entitas;

public sealed class RootSystems : Feature 
{
    public RootSystems(Contexts contexts)
    {
        // Initialize
        Add(new CreateGridSystem(contexts));

        // Execute
        Add(new DrawWalkableTilesSystem(contexts));

        // Cleanup
        Add(new DestroySystem(contexts));

        // Reactive
        Add(new AddViewSystem(contexts));
        Add(new UpdatePathfindingMapSystem(contexts));

        // Entitas generated
        Add(new GameEventSystems(contexts));
    }
}