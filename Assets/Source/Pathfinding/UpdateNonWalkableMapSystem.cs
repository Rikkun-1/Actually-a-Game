using System;
using System.Collections.Generic;
using System.Linq;
using Entitas;
using Roy_T.AStar.Primitives;
using Grid = Roy_T.AStar.Grids.Grid;

public class UpdateNonWalkableMapSystem : ReactiveSystem<GameEntity>
{
    private readonly Contexts _contexts;

    private GameEntity _edgesHolder;
    private GameEntity _gridHolder;

    public UpdateNonWalkableMapSystem(Contexts contexts) : base(contexts.game) => _contexts = contexts;

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.NonWalkable.AddedOrRemoved(),
                                       GameMatcher.NorthWall.AddedOrRemoved(),
                                       GameMatcher.SouthWall.AddedOrRemoved(),
                                       GameMatcher.EastWall.AddedOrRemoved(),
                                       GameMatcher.WestWall.AddedOrRemoved());
    }

    protected override bool Filter(GameEntity entity) {
        return entity.hasPosition;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        _gridHolder = _contexts.game.GetEntities(GameMatcher.PathfindingGrid)
                               .ToList()
                               .SingleEntity();

        _edgesHolder = _contexts.game.GetEntities(GameMatcher.Edges)
                                .ToList()
                                .SingleEntity();

        var grid = _gridHolder.pathfindingGrid.Value;

        foreach (var e in entities)
        {
            var x = e.position.Value.x;
            var y = e.position.Value.y;
            
            if (e.isNonWalkable)    GridChanger.DisconnectNode(x, y, grid);
            else if (e.isEastWall)  GridChanger.WallAdded(grid, x, y, Direction.Right);
            else if (e.isWestWall)  GridChanger.WallAdded(grid, x, y, Direction.Left);
            else if (e.isNorthWall) GridChanger.WallAdded(grid, x, y, Direction.Top);
            else if (e.isSouthWall) GridChanger.WallAdded(grid, x, y, Direction.Bottom);
            else                    GridChanger.ReconnectNode(_contexts.game, grid, x, y);
        }

        _edgesHolder.ReplaceEdges(grid.GetAllEdges());
    }
}

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

        if (canGoDirection(Direction.Top))    grid.AddTwoWayEdge(from,  top,    velocity);
        if (canGoDirection(Direction.Bottom)) grid.AddTwoWayEdge(from,  bottom, velocity);
        if (canGoDirection(Direction.Left))   grid.AddTwoWayEdge(from,  left,   velocity);
        if (canGoDirection(Direction.Right))  grid.AddTwoWayEdge(from,  right,  velocity);
        
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

        bool canGoDirection(Direction direction) =>
            CanGoChecker.CanGoDirection(context, grid, from, direction);
    }

    public static void DisconnectNode(int x, int y, Grid grid)
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

            if (grid.IsInsideGrid(bottom) && grid.IsInsideGrid(left)) grid.RemoveTwoWayEdge(bottom,  left);
            if (grid.IsInsideGrid(bottom) && grid.IsInsideGrid(right)) grid.RemoveTwoWayEdge(bottom, right);
        }
    }
}

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
                return isWalkable(topRight) && isWalkable(top) &&
                       isWalkable(right);
            case Direction.BottomRight:
                return isWalkable(bottomRight) && isWalkable(bottom) &&
                       isWalkable(right);
            case Direction.BottomLeft:
                return isWalkable(bottomLeft) && isWalkable(bottom) &&
                       isWalkable(left);
            case Direction.TopLeft:
                return isWalkable(topLeft) && isWalkable(top) &&
                       isWalkable(left);
            default:
                throw new ArgumentOutOfRangeException(nameof(direction), direction, null);
        }

        bool isWalkable(GridPosition position)
        {
            return context.GetEntitiesWithPosition(position.ToVector2Int())
                          .All(e => !e.isNonWalkable);
        }
    }

    private static bool _noWallsInLateralDirection(GameContext context, GridPosition from,
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
            _                => throw new ArgumentOutOfRangeException(nameof(direction), direction, null)
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

    public static bool NoWallsInDirection(GameContext context, GridPosition from, Direction direction)
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
                return _noWallsInLateralDirection(context, from, direction);

            case Direction.TopRight:
                return _noWallsInLateralDirection(context, from,  Direction.Top) &&
                       _noWallsInLateralDirection(context, from,  Direction.Right) &&
                       _noWallsInLateralDirection(context, top,   Direction.Right) &&
                       _noWallsInLateralDirection(context, right, Direction.Top);

            case Direction.TopLeft:
                return _noWallsInLateralDirection(context, from, Direction.Top) &&
                       _noWallsInLateralDirection(context, from, Direction.Left) &&
                       _noWallsInLateralDirection(context, top,  Direction.Left) &&
                       _noWallsInLateralDirection(context, left, Direction.Top);

            case Direction.BottomRight:
                return _noWallsInLateralDirection(context, from,   Direction.Bottom) &&
                       _noWallsInLateralDirection(context, from,   Direction.Right) &&
                       _noWallsInLateralDirection(context, bottom, Direction.Right) &&
                       _noWallsInLateralDirection(context, right,  Direction.Bottom);

            case Direction.BottomLeft:
                return _noWallsInLateralDirection(context, from,   Direction.Bottom) &&
                       _noWallsInLateralDirection(context, from,   Direction.Left) &&
                       _noWallsInLateralDirection(context, bottom, Direction.Left) &&
                       _noWallsInLateralDirection(context, left,   Direction.Bottom);

            default:
                throw new ArgumentOutOfRangeException(nameof(direction), direction, null);
        }
    }

    public static bool DirectionIsInGrid(Grid grid, GridPosition from, Direction direction)
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
            _                     => throw new ArgumentOutOfRangeException(nameof(direction), direction, null)
        };
    }

    public static bool CanGoDirection(GameContext context, Grid grid, GridPosition from, Direction direction)
        => DirectionIsInGrid(grid, from, direction) &&
           NoNonWalkableInDirection(context, from, direction) &&
           NoWallsInDirection(context, from, direction);
}

public enum Direction
{
    Top,
    TopRight,
    Right,
    BottomRight,
    Bottom,
    BottomLeft,
    Left,
    TopLeft
}