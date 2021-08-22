using System.Collections.Generic;
using Entitas;
using Roy_T.AStar.Grids;
using Roy_T.AStar.Primitives;

public class ResizePathfindingMapSystem : ReactiveSystem<GameEntity>
{
    private readonly Contexts _contexts;

    public  ResizePathfindingMapSystem(Contexts contexts) : base(contexts.game)
    {
        _contexts = contexts;
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
        var grid    = CreateNewMap(mapSize.x, mapSize.y);

        _contexts.game.ReplacePathfindingGrid(grid);
    }

    private Grid CreateNewMap(int columns, int rows)
    {
        var gridSize = new GridSize(columns, rows);
        var cellSize = new Size(Distance.FromMeters(1), Distance.FromMeters(1));

        var traversalVelocity = Velocity.FromMetersPerSecond(2);

        var grid =
            Grid.CreateGridWithLateralAndDiagonalConnections(gridSize, cellSize, traversalVelocity);

        return grid;
    }
}