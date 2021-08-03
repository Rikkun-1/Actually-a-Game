using System.Linq;
using System.Collections;
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
        if(this.gridHolder == null)
        {
            this.gridHolder = this.contexts.game.GetEntities(GameMatcher.PathfindingGrid)
                                    .ToList()
                                    .SingleEntity();
        }

        if(this.edgesHolder == null)
        {
            this.edgesHolder = this.contexts.game.GetEntities(GameMatcher.Edges)
                                        .ToList()
                                        .SingleEntity();
        }

        var grid = this.gridHolder.pathfindingGrid.value;
        
        foreach (var e in entities)
        {
            if(e.isNonWalkable)
            {
                e.ReplaceViewPrefab("nonWalkable");

                var position = e.position.value;
                var gridPosition = new GridPosition(position.x, position.y);
                grid.DisconnectNode(gridPosition);
                grid.RemoveDiagonalConnectionsIntersectingWithNode(gridPosition);
            }
            else
            {
                e.ReplaceViewPrefab("floor");
                var x = e.position.value.x;
                var y = e.position.value.y;
                var gridPosition = new GridPosition(x, y);

                var node = grid.GetNode(gridPosition);

                var velocity = Velocity.FromMetersPerSecond(2);

                Node top         = grid.GetNode(x, y - 1);
                Node bottom      = grid.GetNode(x, y + 1);
                Node left        = grid.GetNode(x - 1, y);
                Node right       = grid.GetNode(x + 1, y);
                Node topLeft     = grid.GetNode(x - 1, y - 1);
                Node topRight    = grid.GetNode(x + 1, y - 1);
                Node bottomLeft  = grid.GetNode(x - 1, y + 1);
                Node bottomRight = grid.GetNode(x + 1, y + 1);

                if (isWalkable(top))    
                    node.Connect(top, velocity);

                if (isWalkable(bottom)) 
                    node.Connect(bottom, velocity);

                if (isWalkable(left))   
                    node.Connect(left, velocity);

                if (isWalkable(right))  
                    node.Connect(right, velocity);


                if (isWalkable(topLeft) && isWalkable(top) && isWalkable(left))
                {
                    node.Connect(topLeft, velocity);
                    top.Connect(left, velocity);
                }

                if (isWalkable(topRight) && isWalkable(top) && isWalkable(right))
                {
                    node.Connect(topRight, velocity);
                    top.Connect(right, velocity);
                }

                if (isWalkable(bottomLeft) && isWalkable(bottom) && isWalkable(left))
                {
                    node.Connect(bottomLeft, velocity);
                    bottom.Connect(left, velocity);
                }

                if (isWalkable(bottomRight) && isWalkable(bottom) && isWalkable(right))
                {
                    node.Connect(bottomRight, velocity);
                    bottom.Connect(right, velocity);
                }

            }
        }
        
        this.edgesHolder.ReplaceEdges(grid.GetAllEdges());

        bool isWalkable(Node node)
        {
            return node != null 
                    ? this.contexts.game.GetEntitiesWithPosition(node.Position.ToVector2Int()).All(e => ! e.isNonWalkable) 
                    : false;
        }
    }
}

public static class PositionExtension
{
    public static Vector2Int ToVector2Int(this Position position)
    {
        return new Vector2Int((int)position.X, (int)position.Y);
    }
}