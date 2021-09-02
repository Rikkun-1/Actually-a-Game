using Entitas;
using Entitas.CodeGeneration.Attributes;

[Game, Unique]
public sealed class GameTickComponent : IComponent
{
    public double deltaTime;
    public int    tickFromStart;
    public double timeFromStart;
}