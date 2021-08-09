using System;
using System.Linq;
using System.Collections.Generic;

using UnityEngine;
using Entitas;

using Grid = Roy_T.AStar.Grids.Grid;
using Roy_T.AStar.Primitives;
using Roy_T.AStar.Graphs;

public class UpdateNonWalkableMapSystem : ReactiveSystem<GameEntity>
{
    readonly Contexts contexts;

    GameEntity gridHolder;
    GameEntity edgesHolder;

    public UpdateNonWalkableMapSystem(Contexts contexts) : base(contexts.game)
    {
        this.contexts = contexts;
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.NonWalkable.AddedOrRemoved());
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

            Node top = grid.GetNode(x, y - 1);
            Node bottom = grid.GetNode(x, y + 1);
            Node left = grid.GetNode(x - 1, y);
            Node right = grid.GetNode(x + 1, y);
            Node topLeft = grid.GetNode(x - 1, y - 1);
            Node topRight = grid.GetNode(x + 1, y - 1);
            Node bottomLeft = grid.GetNode(x - 1, y + 1);
            Node bottomRight = grid.GetNode(x + 1, y + 1);

            bool canGoDirection(Direction direction) => CanGoChecker.CanGoDirection(this.contexts.game, node.Position.ToVector2Int(), direction);

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
                top.Connect(left, velocity);
            }

            if (topRight != null && canGoDirection(Direction.TopRight))
            {
                node.Connect(topRight, velocity);
                top.Connect(right, velocity);
            }

            if (bottomLeft != null && canGoDirection(Direction.BottomLeft))
            {
                node.Connect(bottomLeft, velocity);
                bottom.Connect(left, velocity);
            }

            if (bottomRight != null && canGoDirection(Direction.BottomRight))
            {
                node.Connect(bottomRight, velocity);
                bottom.Connect(right, velocity);
            }
        }
        void disconectNode(int x, int y, Grid grid)
        {
            var gridPosition = new GridPosition(x, y);

            grid.DisconnectNode(gridPosition);
            grid.RemoveDiagonalConnectionsIntersectingWithNode(gridPosition);
        }


        if (this.gridHolder == null)
        {
            this.gridHolder = this.contexts.game.GetEntities(GameMatcher.PathfindingGrid)
                                    .ToList()
                                    .SingleEntity();
        }
        if (this.edgesHolder == null)
        {
            this.edgesHolder = this.contexts.game.GetEntities(GameMatcher.Edges)
                                        .ToList()
                                        .SingleEntity();
        }

        var grid = this.gridHolder.pathfindingGrid.value;

        foreach (var e in entities)
        {
            var x = e.position.value.x;
            var y = e.position.value.y;

            if (e.isNonWalkable)
            {
                e.ReplaceViewPrefab("nonWalkable");
                disconectNode(x, y, grid);
            }
            else
            {
                e.ReplaceViewPrefab("floor");
                reconectNode(x, y, grid);
            }
        }

        this.edgesHolder.ReplaceEdges(grid.GetAllEdges());
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
            switch (direction)
            {
                case Direction.TopRight:
                    return noTopWall(position) && noRightWall(position);
             
                case Direction.BottomRight:
                    return noBottomWall(position) && noRightWall(position);
                
                case Direction.BottomLeft:
                    return noBottomWall(position) && noLeftWall(position);
                
                case Direction.TopLeft:
                    return noTopWall(position) && noLeftWall(position);

                default:
                    throw new ArgumentOutOfRangeException("Direction must be diagonal, but given direction is " + direction);
            }
        }
        bool walkableInDirection(Vector2Int position, Direction direction)
        {
            var x = from.x;
            var y = from.y;

            Vector2Int top = new Vector2Int(x, y - 1);
            Vector2Int bottom = new Vector2Int(x, y + 1);
            Vector2Int left = new Vector2Int(x - 1, y);
            Vector2Int right = new Vector2Int(x + 1, y);
            Vector2Int topLeft = new Vector2Int(x - 1, y - 1);
            Vector2Int topRight = new Vector2Int(x + 1, y - 1);
            Vector2Int bottomLeft = new Vector2Int(x - 1, y + 1);
            Vector2Int bottomRight = new Vector2Int(x + 1, y + 1);

            bool isWalkable(Vector2Int position)
            {
                return context.GetEntitiesWithPosition(position).All(e => !e.isNonWalkable);
            }

            switch (direction)
            {
                case Direction.Top: return isWalkable(top);
                case Direction.Right: return isWalkable(right);
                case Direction.Bottom: return isWalkable(bottom);
                case Direction.Left: return isWalkable(left);
                case Direction.TopRight: return isWalkable(topRight) && isWalkable(top) && isWalkable(right);
                case Direction.BottomRight: return isWalkable(bottomRight) && isWalkable(bottom) && isWalkable(right);
                case Direction.BottomLeft: return isWalkable(bottomLeft) && isWalkable(bottom) && isWalkable(left);
                case Direction.TopLeft: return isWalkable(topLeft) && isWalkable(top) && isWalkable(left);
            }

            return false;
        }
        bool noWallsInDirection(Vector2Int position, Direction direction)
        {
            var x = from.x;
            var y = from.y;

            Vector2Int top = new Vector2Int(x, y - 1);
            Vector2Int bottom = new Vector2Int(x, y + 1);
            Vector2Int left = new Vector2Int(x - 1, y);
            Vector2Int right = new Vector2Int(x + 1, y);
            Vector2Int topLeft = new Vector2Int(x - 1, y - 1);
            Vector2Int topRight = new Vector2Int(x + 1, y - 1);
            Vector2Int bottomLeft = new Vector2Int(x - 1, y + 1);
            Vector2Int bottomRight = new Vector2Int(x + 1, y + 1);

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

        return walkableInDirection(from, direction) && noWallsInDirection(from, direction);
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
