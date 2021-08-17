using Entitas;
using Grid = Roy_T.AStar.Grids.Grid;

[Game]
public class PathfindingGridComponent : IComponent
{
    public Grid Value;
}