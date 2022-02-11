using Entitas;
using UnityEngine;

public class SimulatePhysicsSystem : IExecuteSystem
{
    private int frameDelay = 2;
    private int count;
    
    public SimulatePhysicsSystem(Contexts contexts)
    {
    }
    
    public void Execute()
    {
        count++;
        if (count < frameDelay) return;
        count = 0;
        Physics.Simulate(GameTime.deltaTime * frameDelay);
    }
}