using System.Linq;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using Entitas;

using Grid = Roy_T.AStar.Grids.Grid;
using Roy_T.AStar.Primitives;
using Roy_T.AStar.Graphs;

public class ResizePathfindingMapSystem : ReactiveSystem<GameEntity>
{
    readonly Contexts contexts;

    GameEntity gridHolder;
    GameEntity edgesHolder;

    public ResizePathfindingMapSystem(Contexts contexts) : base(contexts.game)
    {
        this.contexts = contexts;
        this.gridHolder = this.contexts.game.CreateEntity();
        this.edgesHolder = this.contexts.game.CreateEntity();
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.Map.Added());
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.hasMap;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        var mapSize = entities.SingleEntity().map.mapSize;
        var grid = createNewMap(mapSize.x, mapSize.y);
        var edges = getAllWalkableEdgesOnMap(grid);

        this.gridHolder.ReplacePathfindingGrid(grid);
        this.edgesHolder.ReplaceEdges(edges);
    }

    Grid createNewMap(int columns, int rows)
    {
        var gridSize = new GridSize(columns: columns, rows: rows);
        var cellSize = new Size(Distance.FromMeters(1), Distance.FromMeters(1));

        var lateralTraversalVelocity = Velocity.FromMetersPerSecond(2);
        var diagonalTraversalVelocity = lateralTraversalVelocity;

        var grid = Grid.CreateGridWithLateralAndDiagonalConnections(gridSize, cellSize, lateralTraversalVelocity, diagonalTraversalVelocity);
       
        return grid;
    }

    List<IEdge> getAllWalkableEdgesOnMap(Grid grid)
    {
        var nodes = grid.GetAllNodes();
        var edges = new List<IEdge>();

        foreach (var node in nodes)
        {
            edges.AddRange(node.Outgoing);
        }
        edges = edges.Distinct().ToList();

        return edges;
    }
}