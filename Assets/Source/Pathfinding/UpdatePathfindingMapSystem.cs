using System.Linq;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using Entitas;

using Grid = Roy_T.AStar.Grids.Grid;
using Roy_T.AStar.Primitives;
using Roy_T.AStar.Graphs;

public class UpdatePathfindingMapSystem : ReactiveSystem<GameEntity>
{
    readonly Contexts contexts;

    Grid grid;

    public UpdatePathfindingMapSystem(Contexts contexts) : base(contexts.game)
    {
        this.contexts = contexts;
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
        createNewMap(mapSize.x, mapSize.y);

        var edges = getAllWalkableEdgesOnMap();
        updateEntityHoldingEdges(edges);
    }

    void createNewMap(int columns, int rows)
    {
        var gridSize = new GridSize(columns: columns, rows: rows);
        var cellSize = new Size(Distance.FromMeters(1), Distance.FromMeters(1));

        var lateralTraversalVelocity = Velocity.FromMetersPerSecond(2);
        var diagonalTraversalVelocity = lateralTraversalVelocity;

        this.grid = Grid.CreateGridWithLateralAndDiagonalConnections(gridSize, cellSize, lateralTraversalVelocity, diagonalTraversalVelocity);
        
    }

    List<IEdge> getAllWalkableEdgesOnMap()
    {
        var nodes = this.grid.GetAllNodes();
        var edges = new List<IEdge>();

        foreach (var node in nodes)
        {
            edges.AddRange(node.Outgoing);
        }
        edges = edges.Distinct().ToList();

        return edges;
    }

    void updateEntityHoldingEdges(List<IEdge> edges)
    {
        var edgesEntities = contexts.game.GetEntities(GameMatcher.Edges).ToList();

        if (edgesEntities.Count == 0)
        {
            var e = contexts.game.CreateEntity();
            e.AddEdges(edges);
        }
        else
        {
            edgesEntities.SingleEntity().ReplaceEdges(edges);
        }
    }
}