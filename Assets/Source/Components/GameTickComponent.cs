using Entitas;

[Game]
public class GameTick : IComponent
{
    public double deltaTime;
    public int    tickFromStart;
    public double timeFromStart;
}