using Entitas;
using UnityEngine;

public class SimulatePhysicsSystem : IExecuteSystem
{
    public SimulatePhysicsSystem(Contexts contexts)
    {
    }
    
    public void Execute()
    {
        var deltaTime = GameTime.deltaTime;
        if(deltaTime > 0) Physics.Simulate(deltaTime);
    }
}