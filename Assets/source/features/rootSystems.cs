using UnityEngine;
using Entitas;

public sealed class RootSystems : Feature 
{
    public RootSystems(Contexts contexts)
    {
        Add(new CreateGridSystem(contexts));
    }
}