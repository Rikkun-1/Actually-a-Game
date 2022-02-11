using Entitas;
using UnityEngine;

public class SimulatePhysicsSystem : IExecuteSystem
{
    private int _frameDelay = 2;
    private int _frame;
    
    public SimulatePhysicsSystem(Contexts contexts)
    {
    }
    
    public void Execute()
    {
        if (++_frame < _frameDelay) return;
        _frame = 0;
        Physics.Simulate(GameTime.deltaTime * _frameDelay);
    }
}