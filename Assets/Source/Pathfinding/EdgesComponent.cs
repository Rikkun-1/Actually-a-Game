using System.Collections.Generic;
using Entitas;
using Entitas.CodeGeneration.Attributes;
using Roy_T.AStar.Graphs;

[Unique]
public class EdgesComponent : IComponent
{
    public IReadOnlyList<IEdge> value;
}