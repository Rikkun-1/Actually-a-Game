using System;
using System.Linq;
using Roy_T.AStar.Grids;
using Roy_T.AStar.Primitives;

public static class CanGoChecker
{
    public static bool NoNonWalkableInDirection(GameContext context, GridPosition from,
                                                Direction   direction)
    {
        var x = from.X;
        var y = from.Y;

        var top         = new GridPosition(x,     y + 1);
        var bottom      = new GridPosition(x,     y - 1);
        var left        = new GridPosition(x - 1, y);
        var right       = new GridPosition(x + 1, y);
        var topLeft     = new GridPosition(x - 1, y + 1);
        var topRight    = new GridPosition(x + 1, y + 1);
        var bottomLeft  = new GridPosition(x - 1, y - 1);
        var bottomRight = new GridPosition(x + 1, y - 1);

        if (!isWalkable(from)) return false;

        switch (direction)
        {
            case Direction.Top:    return isWalkable(top);
            case Direction.Right:  return isWalkable(right);
            case Direction.Bottom: return isWalkable(bottom);
            case Direction.Left:   return isWalkable(left);

            case Direction.TopRight:
                return isWalkable(topRight) && isWalkable(top) && isWalkable(right);

            case Direction.BottomRight:
                return isWalkable(bottomRight) && isWalkable(bottom) && isWalkable(right);

            case Direction.BottomLeft:
                return isWalkable(bottomLeft) && isWalkable(bottom) && isWalkable(left);

            case Direction.TopLeft:
                return isWalkable(topLeft) && isWalkable(top) && isWalkable(left);

            default:
                throw new ArgumentOutOfRangeException(nameof(direction), direction, null);
        }

        bool isWalkable(GridPosition position)
        {
            return context.GetEntitiesWithPosition(position.ToVector2Int())
                          .All(e => !e.isNonWalkable);
        }
    }

    private static bool NoWallsInLateralDirection(GameContext context, GridPosition from,
                                                  Direction   direction)
    {
        var x = from.X;
        var y = from.Y;

        var top    = new GridPosition(x,     y + 1);
        var bottom = new GridPosition(x,     y - 1);
        var left   = new GridPosition(x - 1, y);
        var right  = new GridPosition(x + 1, y);

        return direction switch
        {
            Direction.Top    => noTopWall(from) && noBottomWall(top),
            Direction.Right  => noRightWall(from) && noLeftWall(right),
            Direction.Bottom => noBottomWall(from) && noTopWall(bottom),
            Direction.Left   => noLeftWall(from) && noRightWall(left),
            _ => throw new ArgumentOutOfRangeException(nameof(direction), direction, null)
        };

        bool noTopWall(GridPosition position)
            => context.GetEntitiesWithPosition(position.ToVector2Int()).All(e => !e.isNorthWall);

        bool noRightWall(GridPosition position)
            => context.GetEntitiesWithPosition(position.ToVector2Int()).All(e => !e.isEastWall);

        bool noBottomWall(GridPosition position)
            => context.GetEntitiesWithPosition(position.ToVector2Int()).All(e => !e.isSouthWall);

        bool noLeftWall(GridPosition position)
            => context.GetEntitiesWithPosition(position.ToVector2Int()).All(e => !e.isWestWall);
    }

    public static bool NoWallsInDirection(GameContext context, GridPosition from,
                                          Direction   direction)
    {
        var x = from.X;
        var y = from.Y;

        var top    = new GridPosition(x,     y + 1);
        var bottom = new GridPosition(x,     y - 1);
        var left   = new GridPosition(x - 1, y);
        var right  = new GridPosition(x + 1, y);

        switch (direction)
        {
            case Direction.Top:
            case Direction.Bottom:
            case Direction.Right:
            case Direction.Left:
                return NoWallsInLateralDirection(context, from, direction);

            case Direction.TopRight:
                return NoWallsInLateralDirection(context, from,  Direction.Top) &&
                       NoWallsInLateralDirection(context, from,  Direction.Right) &&
                       NoWallsInLateralDirection(context, top,   Direction.Right) &&
                       NoWallsInLateralDirection(context, right, Direction.Top);

            case Direction.TopLeft:
                return NoWallsInLateralDirection(context, from, Direction.Top) &&
                       NoWallsInLateralDirection(context, from, Direction.Left) &&
                       NoWallsInLateralDirection(context, top,  Direction.Left) &&
                       NoWallsInLateralDirection(context, left, Direction.Top);

            case Direction.BottomRight:
                return NoWallsInLateralDirection(context, from,   Direction.Bottom) &&
                       NoWallsInLateralDirection(context, from,   Direction.Right) &&
                       NoWallsInLateralDirection(context, bottom, Direction.Right) &&
                       NoWallsInLateralDirection(context, right,  Direction.Bottom);

            case Direction.BottomLeft:
                return NoWallsInLateralDirection(context, from,   Direction.Bottom) &&
                       NoWallsInLateralDirection(context, from,   Direction.Left) &&
                       NoWallsInLateralDirection(context, bottom, Direction.Left) &&
                       NoWallsInLateralDirection(context, left,   Direction.Bottom);

            default:
                throw new ArgumentOutOfRangeException(nameof(direction), direction, null);
        }
    }

    public static bool DirectionIsInsideGrid(Grid grid, GridPosition from, Direction direction)
    {
        var x = from.X;
        var y = from.Y;

        var top         = new GridPosition(x,     y + 1);
        var bottom      = new GridPosition(x,     y - 1);
        var left        = new GridPosition(x - 1, y);
        var right       = new GridPosition(x + 1, y);
        var topLeft     = new GridPosition(x - 1, y + 1);
        var topRight    = new GridPosition(x + 1, y + 1);
        var bottomLeft  = new GridPosition(x - 1, y - 1);
        var bottomRight = new GridPosition(x + 1, y - 1);

        return direction switch
        {
            Direction.Top         => grid.IsInsideGrid(top),
            Direction.TopRight    => grid.IsInsideGrid(topRight),
            Direction.Right       => grid.IsInsideGrid(right),
            Direction.BottomRight => grid.IsInsideGrid(bottomRight),
            Direction.Bottom      => grid.IsInsideGrid(bottom),
            Direction.BottomLeft  => grid.IsInsideGrid(bottomLeft),
            Direction.Left        => grid.IsInsideGrid(left),
            Direction.TopLeft     => grid.IsInsideGrid(topLeft),
            _ => throw new ArgumentOutOfRangeException(nameof(direction), direction, null)
        };
    }

    public static bool CanGoDirection(GameContext context, Grid grid, GridPosition from,
                                      Direction   direction)
        => DirectionIsInsideGrid(grid, from, direction) &&
           NoNonWalkableInDirection(context, from, direction) &&
           NoWallsInDirection(context, from, direction);
}