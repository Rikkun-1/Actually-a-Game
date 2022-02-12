using Entitas;
using UnityEngine;

public class SyncTransformsSystem : IExecuteSystem
{
    public SyncTransformsSystem(Contexts contexts)
    {
    }

    public void Execute()
    {
        Physics.SyncTransforms();
    }
}