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
        return context.CreateCollector(GameMatcher.MapSize.Added());
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.hasMapSize;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        var mapSize = entities.SingleEntity().mapSize.value;
        var grid = createNewMap(mapSize.x, mapSize.y);
        var edges = grid.GetAllEdges();

        this.gridHolder.ReplacePathfindingGrid(grid);
        this.edgesHolder.ReplaceEdges(edges);
    }

    Grid createNewMap(int columns, int rows)
    {
        var gridSize = new GridSize(columns: columns, rows: rows);
        var cellSize = new Size(Distance.FromMeters(1), Distance.FromMeters(1));

        var traversalVelocity = Velocity.FromMetersPerSecond(2);

        var grid = Grid.CreateGridWithLateralAndDiagonalConnections(gridSize, cellSize, traversalVelocity);
       
        return grid;
    }
}