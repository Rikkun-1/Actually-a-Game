using Entitas;
using Entitas.CodeGeneration.Attributes;

[Game] [Unique] [IgnoreSave]
public sealed class SimulationTickComponent : IComponent
{
    public float deltaTime;
    public int   tickFromStart;
    public float timeFromStart;
}