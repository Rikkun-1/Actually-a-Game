using UnityEngine;
using Entitas;

public sealed class RootSystems : Feature 
{
    public RootSystems(Contexts contexts)
    {
        Add(new CreateGridSystem(contexts));
        Add(new PathfindingSystems(contexts));
        Add(new UnityViewSystem(contexts));
        Add(new DestroySystem(contexts));
        Add(new GameEventSystems(contexts));
    }
}