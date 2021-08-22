using Entitas;
using Entitas.CodeGeneration.Attributes;
using Roy_T.AStar.Grids;

[Game] [Unique]
public class PathfindingGridComponent : IComponent
{
    public Grid value;
}