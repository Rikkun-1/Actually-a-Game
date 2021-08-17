using System;
using System.Collections.Generic;
using System.Linq;
using Entitas;
using Roy_T.AStar.Graphs;
using Roy_T.AStar.Primitives;
using UnityEngine;
using Grid = Roy_T.AStar.Grids.Grid;

public class UpdateNonWalkableMapSystem : ReactiveSystem<GameEntity>
{
    private readonly Contexts _contexts;

    private GameEntity _gridHolder;
    private GameEntity _edgesHolder;

    public UpdateNonWalkableMapSystem(Contexts contexts) : base(contexts.game) => _contexts = contexts;

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.NonWalkable.AddedOrRemoved(),
            GameMatcher.NorthWall.AddedOrRemoved(),
            GameMatcher.SouthWall.AddedOrRemoved(),
            GameMatcher.EastWall.AddedOrRemoved(),
            GameMatcher.WestWall.AddedOrRemoved());
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.hasPosition;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        void reconectNode(int x, int y, Grid grid)
        {
            var node = grid.GetNode(x, y);
            var velocity = Velocity.FromMetersPerSecond(2);

            var top = grid.GetNode(x, y + 1);
            var bottom = grid.GetNode(x, y - 1);
            var left = grid.GetNode(x - 1, y);
            var right = grid.GetNode(x + 1, y);
            var topLeft = grid.GetNode(x - 1, y + 1);
            var topRight = grid.GetNode(x + 1, y + 1);
            var bottomLeft = grid.GetNode(x - 1, y - 1);
            var bottomRight = grid.GetNode(x + 1, y - 1);

            bool canGoDirection(Direction direction) => CanGoChecker.CanGoDirection(_contexts.game, node.Position.ToVector2Int(), direction);

            if (top != null && canGoDirection(Direction.Top))
            {
                node.Connect(top, velocity);
            }

            if (bottom != null && canGoDirection(Direction.Bottom))
            {
                node.Connect(bottom, velocity);
            }

            if (left != null && canGoDirection(Direction.Left))
            {
                node.Connect(left, velocity);
            }

            if (right != null && canGoDirection(Direction.Right))
            {
                node.Connect(right, velocity);
            }

            if (topLeft != null && canGoDirection(Direction.TopLeft))
            {
                node.Connect(topLeft, velocity);
                top?.Connect(left, velocity);
            }

            if (topRight != null && canGoDirection(Direction.TopRight))
            {
                node.Connect(topRight, velocity);
                top?.Connect(right, velocity);
            }

            if (bottomLeft != null && canGoDirection(Direction.BottomLeft))
            {
                node.Connect(bottomLeft, velocity);
                bottom?.Connect(left, velocity);
            }

            if (bottomRight != null && canGoDirection(Direction.BottomRight))
            {
                node.Connect(bottomRight, velocity);
                bottom?.Connect(right, velocity);
            }
        }
        void disconectNode(int x, int y, Grid grid)
        {
            var gridPosition = new GridPosition(x, y);

            grid.DisconnectNode(gridPosition);
            grid.RemoveDiagonalConnectionsIntersectingWithNode(gridPosition);
        }

        void rightWallAdded(int x, int y, Grid grid)
        {
            var node = grid.GetNode(x, y);

            var top = grid.GetNode(x, y + 1);
            var bottom = grid.GetNode(x, y - 1);
            var right = grid.GetNode(x + 1, y);
            var topRight = grid.GetNode(x + 1, y + 1);
            var bottomRight = grid.GetNode(x + 1, y - 1);

            if (right != null)
            {
                node.Disconnect(right);
                right.Disconnect(node);

                if (top != null)
                {
                    right.Disconnect(top);
                    top.Disconnect(right);
                }

                if (bottom != null)
                {
                    right.Disconnect(bottom);
                    bottom.Disconnect(right);
                }
            }

            if (topRight != null)
            {
                node.Disconnect(topRight);
                topRight.Disconnect(node);
            }

            if (bottomRight != null)
            {
                node.Disconnect(bottomRight);
                bottomRight.Disconnect(node);
            }
        }
        void leftWallAdded(int x, int y, Grid grid)
        {
            var node = grid.GetNode(x, y);

            var top = grid.GetNode(x, y + 1);
            var bottom = grid.GetNode(x, y - 1);
            var left = grid.GetNode(x - 1, y);
            var topLeft = grid.GetNode(x - 1, y + 1);
            var bottomLeft = grid.GetNode(x - 1, y - 1);

            if (left != null)
            {
                node.Disconnect(left);
                left.Disconnect(node);

                if (top != null)
                {
                    left.Disconnect(top);
                    top.Disconnect(left);
                }

                if (bottom != null)
                {
                    left.Disconnect(bottom);
                    bottom.Disconnect(left);
                }
            }

            if (topLeft != null)
            {
                node.Disconnect(topLeft);
                topLeft.Disconnect(node);
            }

            if (bottomLeft != null)
            {
                node.Disconnect(bottomLeft);
                bottomLeft.Disconnect(node);
            }
        }
        void topWallAdded(int x, int y, Grid grid)
        {
            var node = grid.GetNode(x, y);

            var top = grid.GetNode(x, y + 1);
            var left = grid.GetNode(x - 1, y);
            var right = grid.GetNode(x + 1, y);
            var topLeft = grid.GetNode(x - 1, y + 1);
            var topRight = grid.GetNode(x + 1, y + 1);

            if (top != null)
            {
                node.Disconnect(top);
                top.Disconnect(node);

                if (left != null)
                {
                    top.Disconnect(left);
                    left.Disconnect(top);
                }

                if (right != null)
                {
                    top.Disconnect(right);
                    right.Disconnect(top);
                }
            }

            if (topLeft != null)
            {
                node.Disconnect(topLeft);
                topLeft.Disconnect(node);
            }

            if (topRight != null)
            {
                node.Disconnect(topRight);
                topRight.Disconnect(node);
            }
        }
        void bottomWallAdded(int x, int y, Grid grid)
        {
            var node = grid.GetNode(x, y);

            var bottom = grid.GetNode(x, y - 1);
            var left = grid.GetNode(x - 1, y);
            var right = grid.GetNode(x + 1, y);
            var bottomLeft = grid.GetNode(x - 1, y - 1);
            var bottomRight = grid.GetNode(x + 1, y - 1);

            if (bottom != null)
            {
                node.Disconnect(bottom);
                bottom.Disconnect(node);

                if (left != null)
                {
                    bottom.Disconnect(left);
                    left.Disconnect(bottom);
                }

                if (right != null)
                {
                    bottom.Disconnect(right);
                    right.Disconnect(bottom);
                }
            }

            if (bottomLeft != null)
            {
                node.Disconnect(bottomLeft);
                bottomLeft.Disconnect(node);
            }

            if (bottomRight != null)
            {
                node.Disconnect(bottomRight);
                bottomRight.Disconnect(node);
            }
        }

        if (_gridHolder == null)
        {
            _gridHolder = _contexts.game.GetEntities(GameMatcher.PathfindingGrid)
                .ToList()
                .SingleEntity();
        }
        if (_edgesHolder == null)
        {
            _edgesHolder = _contexts.game.GetEntities(GameMatcher.Edges)
                .ToList()
                .SingleEntity();
        }

        var grid = _gridHolder.pathfindingGrid.Value;

        foreach (var e in entities) 
        {
            var x = e.position.Value.x;
            var y = e.position.Value.y;

            if (e.isNonWalkable)
            {
                disconectNode(x, y, grid);
            }
            else if (e.isEastWall)
            {
                rightWallAdded(x, y, grid);
            }
            else if (e.isWestWall)
            {
                leftWallAdded(x, y, grid);
            }
            else if (e.isNorthWall)
            {
                topWallAdded(x, y, grid);
            }
            else if (e.isSouthWall)
            {
                bottomWallAdded(x, y, grid);
            }
            else
            {
                reconectNode(x, y, grid);
            }
        
        }

        _edgesHolder.ReplaceEdges(grid.GetAllEdges());
    }
}

public static class CanGoChecker
{
    public static bool CanGoDirection(GameContext context, Vector2Int from, Direction direction)
    {
        bool NoTopWall(Vector2Int position) => context.GetEntitiesWithPosition(position).All(e => !e.isNorthWall);
        bool NoRightWall(Vector2Int position) => context.GetEntitiesWithPosition(position).All(e => !e.isEastWall);
        bool NoBottomWall(Vector2Int position) => context.GetEntitiesWithPosition(position).All(e => !e.isSouthWall);
        bool NoLeftWall(Vector2Int position) => context.GetEntitiesWithPosition(position).All(e => !e.isWestWall);

        bool NoWallsInCorner(Vector2Int position, Direction direction)
        {
            return direction switch
            {
                Direction.TopRight => NoTopWall(position) && NoRightWall(position),
                Direction.BottomRight => NoBottomWall(position) && NoRightWall(position),
                Direction.BottomLeft => NoBottomWall(position) && NoLeftWall(position),
                Direction.TopLeft => NoTopWall(position) && NoLeftWall(position),
                _ => throw new ArgumentOutOfRangeException("Direction must be diagonal, but given direction is " +
                                                           direction)
            };
        }
        bool WalkableInDirection(Direction direction)
        {
            var x = from.x;
            var y = from.y;

            Vector2Int top = new Vector2Int(x, y + 1);
            Vector2Int bottom = new Vector2Int(x, y - 1);
            Vector2Int left = new Vector2Int(x - 1, y);
            Vector2Int right = new Vector2Int(x + 1, y);
            Vector2Int topLeft = new Vector2Int(x - 1, y + 1);
            Vector2Int topRight = new Vector2Int(x + 1, y + 1);
            Vector2Int bottomLeft = new Vector2Int(x - 1, y - 1);
            Vector2Int bottomRight = new Vector2Int(x + 1, y - 1);

            bool IsWalkable(Vector2Int position)
            {
                return context.GetEntitiesWithPosition(position).All(e => !e.isNonWalkable);
            }

            if (IsWalkable(from))
            {
                switch (direction)
                {
                    case Direction.Top: return IsWalkable(top);
                    case Direction.Right: return IsWalkable(right);
                    case Direction.Bottom: return IsWalkable(bottom);
                    case Direction.Left: return IsWalkable(left);
                    case Direction.TopRight: return IsWalkable(topRight) && IsWalkable(top) && IsWalkable(right);
                    case Direction.BottomRight: return IsWalkable(bottomRight) && IsWalkable(bottom) && IsWalkable(right);
                    case Direction.BottomLeft: return IsWalkable(bottomLeft) && IsWalkable(bottom) && IsWalkable(left);
                    case Direction.TopLeft: return IsWalkable(topLeft) && IsWalkable(top) && IsWalkable(left);
                }
            }

            return false;
        }
        bool NoWallsInDirection(Direction direction)
        {
            var x = from.x;
            var y = from.y;

            var top = new Vector2Int(x, y + 1);
            var bottom = new Vector2Int(x, y - 1);
            var left = new Vector2Int(x - 1, y);
            var right = new Vector2Int(x + 1, y);
            var topLeft = new Vector2Int(x - 1, y + 1);
            var topRight = new Vector2Int(x + 1, y + 1);
            var bottomLeft = new Vector2Int(x - 1, y - 1);
            var bottomRight = new Vector2Int(x + 1, y - 1);

            switch (direction)
            {
                case Direction.Top:
                    return NoTopWall(from) && NoBottomWall(top);

                case Direction.Right:
                    return NoRightWall(from) && NoLeftWall(right);

                case Direction.Bottom:
                    return NoBottomWall(from) && NoTopWall(bottom);

                case Direction.Left:
                    return NoLeftWall(from) && NoRightWall(left);

                case Direction.TopRight:
                    return NoWallsInCorner(from, Direction.TopRight) &&
                           NoWallsInCorner(top, Direction.BottomRight) &&
                           NoWallsInCorner(right, Direction.TopLeft) &&
                           NoWallsInCorner(topRight, Direction.BottomLeft);

                case Direction.BottomRight:
                    return NoWallsInCorner(from, Direction.BottomRight) &&
                           NoWallsInCorner(bottom, Direction.TopRight) &&
                           NoWallsInCorner(right, Direction.BottomLeft) &&
                           NoWallsInCorner(bottomRight, Direction.TopLeft);

                case Direction.BottomLeft:
                    return NoWallsInCorner(from, Direction.BottomLeft) &&
                           NoWallsInCorner(bottom, Direction.TopLeft) &&
                           NoWallsInCorner(left, Direction.BottomRight) &&
                           NoWallsInCorner(bottomLeft, Direction.TopRight);

                case Direction.TopLeft:
                    return NoWallsInCorner(from, Direction.TopLeft) &&
                           NoWallsInCorner(top, Direction.BottomLeft) &&
                           NoWallsInCorner(left, Direction.TopRight) &&
                           NoWallsInCorner(topLeft, Direction.BottomRight);
            }

            return false;
        }

        return WalkableInDirection(direction) && NoWallsInDirection(direction);
    }
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

public static class PositionExtension
{
    public static Vector2Int ToVector2Int(this Position position)
    {
        return new Vector2Int((int)position.X, (int)position.Y);
    }
}