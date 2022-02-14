using System;
using Roy_T.AStar.Grids;
using Roy_T.AStar.Primitives;

public static class GridChanger
{
    public static void ReconnectNode(GameContext game, Grid grid, int x, int y)
    {
        var velocity = Velocity.FromMetersPerSecond(2);

        var from        = new GridPosition(x,     y);
        var top         = new GridPosition(x,     y + 1);
        var bottom      = new GridPosition(x,     y - 1);
        var left        = new GridPosition(x - 1, y);
        var right       = new GridPosition(x + 1, y);
        var topLeft     = new GridPosition(x - 1, y + 1);
        var topRight    = new GridPosition(x + 1, y + 1);
        var bottomLeft  = new GridPosition(x - 1, y - 1);
        var bottomRight = new GridPosition(x + 1, y - 1);

        if (canGoDirection(Direction.Top))    grid.AddTwoWayEdge(from, top,    velocity);
        if (canGoDirection(Direction.Left))   grid.AddTwoWayEdge(from, left,   velocity);
        if (canGoDirection(Direction.Right))  grid.AddTwoWayEdge(from, right,  velocity);
        if (canGoDirection(Direction.Bottom)) grid.AddTwoWayEdge(from, bottom, velocity);

        if (canGoDirection(Direction.TopLeft))     grid.AddCrossWiseTwoWayEdge((from, topLeft),     (top, left),     velocity);
        if (canGoDirection(Direction.TopRight))    grid.AddCrossWiseTwoWayEdge((from, topRight),    (top, right),    velocity);
        if (canGoDirection(Direction.BottomLeft))  grid.AddCrossWiseTwoWayEdge((from, bottomLeft),  (bottom, left),  velocity);
        if (canGoDirection(Direction.BottomRight)) grid.AddCrossWiseTwoWayEdge((from, bottomRight), (bottom, right), velocity);

        bool canGoDirection(Direction direction) => CanGoChecker.CanGoDirection(game, grid, from, direction);
    }

    public static void DisconnectNode(Grid grid, int x, int y)
    {
        var gridPosition = new GridPosition(x, y);

        grid.DisconnectNode(gridPosition);
        grid.RemoveDiagonalConnectionsIntersectingWithNode(gridPosition);
    }

    private static void DisconnectSide(Grid grid, GridPosition position, Direction direction)
    {
        var leftDirection = direction.GetNeighbours().left.GetNeighbours().left;
        var rightDirection = direction.GetNeighbours().right.GetNeighbours().right;
        
        var beforeWallLeft = position.StepInDirection(leftDirection);
        var beforeWallRight = position.StepInDirection(rightDirection);
        
        var behindWall     = position.StepInDirection(direction);
        var behindWallLeft = behindWall.StepInDirection(leftDirection);
        var behindWallRight = behindWall.StepInDirection(rightDirection);
        
        if (grid.IsInsideGrid(behindWall))      grid.RemoveTwoWayEdge(position, behindWall);
        if (grid.IsInsideGrid(behindWallLeft))  grid.RemoveTwoWayEdge(position, behindWallLeft);
        if (grid.IsInsideGrid(behindWallRight)) grid.RemoveTwoWayEdge(position, behindWallRight);
        
        if (grid.IsInsideGrid(beforeWallLeft) && grid.IsInsideGrid(behindWall))  grid.RemoveTwoWayEdge(beforeWallLeft,  behindWall);
        if (grid.IsInsideGrid(beforeWallRight) && grid.IsInsideGrid(behindWall)) grid.RemoveTwoWayEdge(beforeWallRight, behindWall);
    }
    
    public static void WallAdded(Grid grid, int x, int y, Direction direction)
    {
        if (direction.IsLateralDirection()) DisconnectSide(grid, new GridPosition(x, y), direction);
        else throw new ArgumentOutOfRangeException(nameof(direction), direction, null);
    }
}