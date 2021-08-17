using System.Collections.Generic;
using Entitas;
using Roy_T.AStar.Primitives;
using Grid = Roy_T.AStar.Grids.Grid;

public class ResizePathfindingMapSystem : ReactiveSystem<GameEntity>
{
    private GameEntity _gridHolder;
    private GameEntity _edgesHolder;

    public ResizePathfindingMapSystem(Contexts contexts) : base(contexts.game)
    {
        _gridHolder = contexts.game.CreateEntity();
        _edgesHolder = contexts.game.CreateEntity();
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
        var mapSize = entities.SingleEntity().mapSize.Value;
        var grid = CreateNewMap(mapSize.x, mapSize.y);
        var edges = grid.GetAllEdges();

        _gridHolder.ReplacePathfindingGrid(grid);
        _edgesHolder.ReplaceEdges(edges);
    }

    private Grid CreateNewMap(int columns, int rows)
    {
        var gridSize = new GridSize(columns: columns, rows: rows);
        var cellSize = new Size(Distance.FromMeters(1), Distance.FromMeters(1));

        var traversalVelocity = Velocity.FromMetersPerSecond(2);

        var grid = Grid.CreateGridWithLateralAndDiagonalConnections(gridSize, cellSize, traversalVelocity);

        return grid;
    }
}