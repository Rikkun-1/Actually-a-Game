using Entitas;
using Entitas.CodeGeneration.Attributes;
using GraphProcessor;

[Game] [Unique]
public sealed class AIGraphComponent : IComponent
{
    public BaseGraph graph;
}