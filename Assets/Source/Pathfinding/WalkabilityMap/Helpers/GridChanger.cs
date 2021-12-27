using System;
using Roy_T.AStar.Grids;
using Roy_T.AStar.Primitives;
using Source.Pathfinding.WalkabilityMap.Helpers;

public static class GridChanger
{
    public static void ReconnectNode(GameContext context, Grid grid, int x, int y)
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
        if (canGoDirection(Direction.Bottom)) grid.AddTwoWayEdge(from, bottom, velocity);
        if (canGoDirection(Direction.Left))   grid.AddTwoWayEdge(from, left,   velocity);
        if (canGoDirection(Direction.Right))  grid.AddTwoWayEdge(from, right,  velocity);

        if (canGoDirection(Direction.TopLeft))
        {
            grid.AddTwoWayEdge(from, topLeft, velocity);
            grid.AddTwoWayEdge(top,  left,    velocity);
        }

        if (canGoDirection(Direction.TopRight))
        {
            grid.AddTwoWayEdge(from, topRight, velocity);
            grid.AddTwoWayEdge(top,  right,    velocity);
        }

        if (canGoDirection(Direction.BottomLeft))
        {
            grid.AddTwoWayEdge(from,   bottomLeft, velocity);
            grid.AddTwoWayEdge(bottom, left,       velocity);
        }

        if (canGoDirection(Direction.BottomRight))
        {
            grid.AddTwoWayEdge(from,   bottomRight, velocity);
            grid.AddTwoWayEdge(bottom, right,       velocity);
        }

        bool canGoDirection(Direction direction) => CanGoChecker.CanGoDirection(context, grid, from, direction);
    }

    public static void DisconnectNode(Grid grid, int x, int y)
    {
        var gridPosition = new GridPosition(x, y);

        grid.DisconnectNode(gridPosition);
        grid.RemoveDiagonalConnectionsIntersectingWithNode(gridPosition);
    }

    public static void WallAdded(Grid grid, int x, int y, Direction direction)
    {
        var center      = new GridPosition(x,     y);
        var top         = new GridPosition(x,     y + 1);
        var bottom      = new GridPosition(x,     y - 1);
        var left        = new GridPosition(x - 1, y);
        var right       = new GridPosition(x + 1, y);
        var topLeft     = new GridPosition(x - 1, y + 1);
        var topRight    = new GridPosition(x + 1, y + 1);
        var bottomLeft  = new GridPosition(x - 1, y - 1);
        var bottomRight = new GridPosition(x + 1, y - 1);

        if      (direction == Direction.Top)    topWallAdded();
        else if (direction == Direction.Right)  rightWallAdded();
        else if (direction == Direction.Bottom) bottomWallAdded();
        else if (direction == Direction.Left)   leftWallAdded();
        else throw new ArgumentOutOfRangeException(nameof(direction), direction, null);

        void rightWallAdded()
        {
            if (grid.IsInsideGrid(right))       grid.RemoveTwoWayEdge(center, right);
            if (grid.IsInsideGrid(topRight))    grid.RemoveTwoWayEdge(center, topRight);
            if (grid.IsInsideGrid(bottomRight)) grid.RemoveTwoWayEdge(center, bottomRight);

            if (grid.IsInsideGrid(right) && grid.IsInsideGrid(top))    grid.RemoveTwoWayEdge(right, top);
            if (grid.IsInsideGrid(right) && grid.IsInsideGrid(bottom)) grid.RemoveTwoWayEdge(right, bottom);
        }

        void leftWallAdded()
        {
            if (grid.IsInsideGrid(left))       grid.RemoveTwoWayEdge(center, left);
            if (grid.IsInsideGrid(topLeft))    grid.RemoveTwoWayEdge(center, topLeft);
            if (grid.IsInsideGrid(bottomLeft)) grid.RemoveTwoWayEdge(center, bottomLeft);

            if (grid.IsInsideGrid(left) && grid.IsInsideGrid(top))    grid.RemoveTwoWayEdge(left, top);
            if (grid.IsInsideGrid(left) && grid.IsInsideGrid(bottom)) grid.RemoveTwoWayEdge(left, bottom);
        }

        void topWallAdded()
        {
            if (grid.IsInsideGrid(top))      grid.RemoveTwoWayEdge(center, top);
            if (grid.IsInsideGrid(topLeft))  grid.RemoveTwoWayEdge(center, topLeft);
            if (grid.IsInsideGrid(topRight)) grid.RemoveTwoWayEdge(center, topRight);

            if (grid.IsInsideGrid(top) && grid.IsInsideGrid(left))  grid.RemoveTwoWayEdge(top, left);
            if (grid.IsInsideGrid(top) && grid.IsInsideGrid(right)) grid.RemoveTwoWayEdge(top, right);
        }

        void bottomWallAdded()
        {
            if (grid.IsInsideGrid(bottom))      grid.RemoveTwoWayEdge(center, bottom);
            if (grid.IsInsideGrid(bottomLeft))  grid.RemoveTwoWayEdge(center, bottomLeft);
            if (grid.IsInsideGrid(bottomRight)) grid.RemoveTwoWayEdge(center, bottomRight);

            if (grid.IsInsideGrid(bottom) && grid.IsInsideGrid(left))  grid.RemoveTwoWayEdge(bottom, left);
            if (grid.IsInsideGrid(bottom) && grid.IsInsideGrid(right)) grid.RemoveTwoWayEdge(bottom, right);
        }
    }
}