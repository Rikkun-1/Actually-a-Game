using UnityEngine;
using Entitas;

[Game]
public class GameTick : IComponent
{
    public double TimeFromStart;
    public double DeltaTime;
    public int TickFromStart;
}