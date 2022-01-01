using Entitas;
using Entitas.CodeGeneration.Attributes;
using Roy_T.AStar.Grids;

[Game] [Unique]
public sealed class PathfindingGridComponent : IComponent
{
    public Grid value;
}