using Entitas;
using Entitas.CodeGeneration.Attributes;
using GraphProcessor;

[Game] [Unique] [IgnoreSave]
public sealed class AIGraphComponent : IComponent
{
    public BaseGraph graph;
}