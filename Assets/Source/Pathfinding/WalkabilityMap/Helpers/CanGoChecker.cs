using System;
using System.Linq;
using Roy_T.AStar.Grids;
using Roy_T.AStar.Primitives;

public static class CanGoChecker
{
    private static bool IsWalkable(GameContext game, GridPosition position)
    {
        return game.GetEntitiesWithGridPosition(position.ToVector2Int())
                   .All(e => !e.isNonWalkable);
    }

    private static bool CheckNonWalkableInLateralDirection(GameContext game, GridPosition from, Direction   direction)
    {
        var to = from.StepInDirection(direction);
        return IsWalkable(game, to);
    }

    private static bool CheckNonWalkableInDiagonalDirection(GameContext game, GridPosition from, Direction   direction)
    {
        var to = from.StepInDirection(direction);
        
        var (firstSubDirection, secondSubDirection) = direction.GetNeighbours();

        return IsWalkable(game, to) &&
               IsWalkable(game, from.StepInDirection(firstSubDirection)) &&
               IsWalkable(game, from.StepInDirection(secondSubDirection));
    }

    private static bool NoNonWalkableInDirection(GameContext game, GridPosition from,
                                                 Direction   direction)
    {
        if (!IsWalkable(game, from)) return false;

        if (direction.IsLateralDirection())  return CheckNonWalkableInLateralDirection(game, from, direction);
        if (direction.IsDiagonalDirection()) return CheckNonWalkableInDiagonalDirection(game, from, direction);
        throw new ArgumentOutOfRangeException(nameof(direction), direction, null);
    }

    private static bool NoWallInDirection(GameContext game, GridPosition position, Direction direction)
    {
        if(!direction.IsLateralDirection()) throw new ArgumentOutOfRangeException(nameof(direction), direction, null);

        return game.GetEntitiesWithGridPosition(position.ToVector2Int())
                   .All(e => !e.hasWall || e.wall.direction != direction);
    }

    private static bool NoWallsInLateralDirectionTwoWay(GameContext game, GridPosition from, Direction direction)
    {
        var to = from.StepInDirection(direction);

        if(!direction.IsLateralDirection()) throw new ArgumentOutOfRangeException(nameof(direction), direction, null);

        return NoWallInDirection(game, from, direction) &&
               NoWallInDirection(game, to,   direction.OppositeDirection());
    }

    private static bool NoWallInDiagonalDirection(GameContext game, GridPosition from, Direction direction)
    {
        var (firstSubDirection, secondSubDirection) = direction.GetNeighbours();
        var firstSubPosition  = from.StepInDirection(firstSubDirection);
        var secondSubPosition = from.StepInDirection(secondSubDirection);

        if (direction.IsDiagonalDirection()) return NoWallsInLateralDirectionTwoWay(game, from,              firstSubDirection) &&
                                                    NoWallsInLateralDirectionTwoWay(game, from,              secondSubDirection) &&
                                                    NoWallsInLateralDirectionTwoWay(game, firstSubPosition,  secondSubDirection) &&
                                                    NoWallsInLateralDirectionTwoWay(game, secondSubPosition, firstSubDirection);

        throw new ArgumentOutOfRangeException(nameof(direction), direction, null);
    }
    
    private static bool NoWallsInDirection(GameContext game, GridPosition from,
                                           Direction   direction)
    {
        if (direction.IsLateralDirection())  return NoWallsInLateralDirectionTwoWay(game, from, direction);
        if (direction.IsDiagonalDirection()) return NoWallInDiagonalDirection(game, from, direction);
        throw new ArgumentOutOfRangeException(nameof(direction), direction, null);
    }

    private static bool DirectionIsInsideGrid(Grid grid, GridPosition from, Direction direction)
    {
        var to = from.StepInDirection(direction);
        return grid.IsInsideGrid(to);
    }

    public static bool CanGoDirection(GameContext game, Grid grid, GridPosition from, Direction direction)
    {
        return DirectionIsInsideGrid(grid, from, direction) &&
               NoNonWalkableInDirection(game, from, direction) &&
               NoWallsInDirection(game, from, direction);
    }
}