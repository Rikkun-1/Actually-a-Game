using UnityEngine;
using Entitas;

public sealed class RootSystems : Feature 
{
    public RootSystems(Contexts contexts)
    {
        // Initialize
        Add(new CreateGridSystem(contexts));

        // Execute

        // Reactive
        Add(new AddViewSystem(contexts));
        Add(new DestroySystem(contexts));

        // Other
        Add(new GameEventSystems(contexts));
    }
}