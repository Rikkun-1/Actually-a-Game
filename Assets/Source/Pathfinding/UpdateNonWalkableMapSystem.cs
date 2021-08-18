using System;
using System.Collections.Generic;
using System.Linq;
using Entitas;
using Roy_T.AStar.Primitives;
using UnityEngine;
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

    protected override bool Filter(GameEntity entity)
    {
        return entity.hasPosition;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        void reconnectNode(int x, int y, Grid grid)
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

            bool canGoDirection(Direction direction) =>
                CanGoChecker.CanGoDirection(_contexts.game, node.Position.ToVector2Int(), direction);

            if (top != null && canGoDirection(Direction.Top))
            {
                node.Connect(top, velocity);
                top.Connect(node, velocity);
            }

            if (bottom != null && canGoDirection(Direction.Bottom))
            {
                node.Connect(bottom, velocity);
                bottom.Connect(node, velocity);
            }

            if (left != null && canGoDirection(Direction.Left))
            {
                node.Connect(left, velocity);
                left.Connect(node, velocity);
            }

            if (right != null && canGoDirection(Direction.Right))
            {
                node.Connect(right, velocity);
                right.Connect(node, velocity);
            }

            if (topLeft != null && canGoDirection(Direction.TopLeft))
            {
                node.Connect(topLeft, velocity);
                topLeft.Connect(node, velocity);
                top?.Connect(left, velocity);
                left?.Connect(top, velocity);
            }

            if (topRight != null && canGoDirection(Direction.TopRight))
            {
                node.Connect(topRight, velocity);
                topRight.Connect(node, velocity);
                top?.Connect(right, velocity);
                right?.Connect(top, velocity);
            }

            if (bottomLeft != null && canGoDirection(Direction.BottomLeft))
            {
                node.Connect(bottomLeft, velocity);
                bottomLeft.Connect(node, velocity);
                bottom?.Connect(left, velocity);
                left?.Connect(bottom, velocity);
            }

            if (bottomRight != null && canGoDirection(Direction.BottomRight))
            {
                node.Connect(bottomRight, velocity);
                bottomRight.Connect(node, velocity);
                bottom?.Connect(right, velocity);
                right?.Connect(bottom, velocity);
            }
        }

        void disconnectNode(int x, int y, Grid grid)
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

            if (e.isNonWalkable)
            {
                disconnectNode(x, y, grid);
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
                reconnectNode(x, y, grid);
            }
        }

        _edgesHolder.ReplaceEdges(grid.GetAllEdges());
    }
}

public static class CanGoChecker
{
    public static bool CanGoDirection(GameContext context, Vector2Int from, Direction direction)
    {
        bool noTopWall(Vector2Int position) => context.GetEntitiesWithPosition(position).All(e => !e.isNorthWall);
        bool noRightWall(Vector2Int position) => context.GetEntitiesWithPosition(position).All(e => !e.isEastWall);
        bool noBottomWall(Vector2Int position) => context.GetEntitiesWithPosition(position).All(e => !e.isSouthWall);
        bool noLeftWall(Vector2Int position) => context.GetEntitiesWithPosition(position).All(e => !e.isWestWall);

        bool noWallsInCorner(Vector2Int position, Direction direction)
        {
            return direction switch
            {
                Direction.TopRight => noTopWall(position) && noRightWall(position),
                Direction.BottomRight => noBottomWall(position) && noRightWall(position),
                Direction.BottomLeft => noBottomWall(position) && noLeftWall(position),
                Direction.TopLeft => noTopWall(position) && noLeftWall(position),
                _ => throw new ArgumentOutOfRangeException("Direction must be diagonal, but given direction is " +
                                                           direction)
            };
        }

        bool walkableInDirection(Direction direction)
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

            bool isWalkable(Vector2Int position)
            {
                return context.GetEntitiesWithPosition(position).All(e => !e.isNonWalkable);
            }

            if (isWalkable(from))
            {
                switch (direction)
                {
                    case Direction.Top: return isWalkable(top);
                    case Direction.Right: return isWalkable(right);
                    case Direction.Bottom: return isWalkable(bottom);
                    case Direction.Left: return isWalkable(left);
                    case Direction.TopRight: return isWalkable(topRight) && isWalkable(top) && isWalkable(right);
                    case Direction.BottomRight:
                        return isWalkable(bottomRight) && isWalkable(bottom) && isWalkable(right);
                    case Direction.BottomLeft: return isWalkable(bottomLeft) && isWalkable(bottom) && isWalkable(left);
                    case Direction.TopLeft: return isWalkable(topLeft) && isWalkable(top) && isWalkable(left);
                }
            }

            return false;
        }

        bool noWallsInDirection(Direction direction)
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
                    return noTopWall(from) && noBottomWall(top);

                case Direction.Right:
                    return noRightWall(from) && noLeftWall(right);

                case Direction.Bottom:
                    return noBottomWall(from) && noTopWall(bottom);

                case Direction.Left:
                    return noLeftWall(from) && noRightWall(left);

                case Direction.TopRight:
                    return noWallsInCorner(from, Direction.TopRight) &&
                           noWallsInCorner(top, Direction.BottomRight) &&
                           noWallsInCorner(right, Direction.TopLeft) &&
                           noWallsInCorner(topRight, Direction.BottomLeft);

                case Direction.BottomRight:
                    return noWallsInCorner(from, Direction.BottomRight) &&
                           noWallsInCorner(bottom, Direction.TopRight) &&
                           noWallsInCorner(right, Direction.BottomLeft) &&
                           noWallsInCorner(bottomRight, Direction.TopLeft);

                case Direction.BottomLeft:
                    return noWallsInCorner(from, Direction.BottomLeft) &&
                           noWallsInCorner(bottom, Direction.TopLeft) &&
                           noWallsInCorner(left, Direction.BottomRight) &&
                           noWallsInCorner(bottomLeft, Direction.TopRight);

                case Direction.TopLeft:
                    return noWallsInCorner(from, Direction.TopLeft) &&
                           noWallsInCorner(top, Direction.BottomLeft) &&
                           noWallsInCorner(left, Direction.TopRight) &&
                           noWallsInCorner(topLeft, Direction.BottomRight);
            }

            return false;
        }

        return walkableInDirection(direction) && noWallsInDirection(direction);
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