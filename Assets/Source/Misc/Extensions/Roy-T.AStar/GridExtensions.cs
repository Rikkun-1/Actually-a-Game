using Roy_T.AStar.Grids;
using Roy_T.AStar.Primitives;

public static class GridExtensions
{
    public static void AddCrossWiseTwoWayEdge(this Grid                    grid,
                                              (GridPosition, GridPosition) firstPair,
                                              (GridPosition, GridPosition) secondPair,
                                              Velocity                     velocity)
    {
        grid.AddTwoWayEdge(firstPair.Item1,  firstPair.Item2,  velocity);
        grid.AddTwoWayEdge(secondPair.Item1, secondPair.Item2, velocity);
    }
    
    public static void RemoveTwoWayEdge(this Grid grid, GridPosition from, GridPosition to)
    {
        grid.RemoveEdge(from, to);
        grid.RemoveEdge(to,   from);
    }
        
    public static void AddTwoWayEdge(this Grid grid, GridPosition from, GridPosition to, Velocity traversalVelocity)
    {
        grid.AddEdge(from, to,   traversalVelocity);
        grid.AddEdge(to,   from, traversalVelocity);
    }
}
