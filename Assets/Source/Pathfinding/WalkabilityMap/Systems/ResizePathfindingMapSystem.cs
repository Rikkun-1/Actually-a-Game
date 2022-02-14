using System.Collections.Generic;
using Entitas;
using Roy_T.AStar.Grids;
using Roy_T.AStar.Primitives;

public class ResizePathfindingMapSystem : ReactiveSystem<GameEntity>
{
    private readonly GameContext _game;

    public  ResizePathfindingMapSystem(Contexts contexts) : base(contexts.game)
    {
        _game = contexts.game;
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.GridSize.Added());
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.hasGridSize;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        var gridSize = _game.gridSize.value;
        var grid     = CreateNewMap(gridSize.x, gridSize.y);

        _game.ReplacePathfindingGrid(grid);
    }

    private static Grid CreateNewMap(int columns, int rows)
    {
        var gridSize = new GridSize(columns, rows);
        var cellSize = new Size(Distance.FromMeters(1), Distance.FromMeters(1));

        var traversalVelocity = Velocity.FromMetersPerSecond(2);

        var grid = Grid.CreateGridWithLateralAndDiagonalConnections(gridSize, cellSize, traversalVelocity);

        return grid;
    }
}