using System.Collections.Generic;
using Entitas;

public class UpdateWallMapSystem : ReactiveSystem<GameEntity>
{
    private readonly GameContext _game;

    public UpdateWallMapSystem(Contexts contexts) : base(contexts.game)
    {
        _game = contexts.game;
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.Wall.AddedOrRemoved());
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.hasGridPosition;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        var pathfindingGrid = _game.pathfindingGrid.value;

        foreach (var e in entities)
        {
            var x = e.gridPosition.value.x;
            var y = e.gridPosition.value.y;

            if (e.hasWall) GridChanger.WallAdded(pathfindingGrid, x, y, e.wall.direction);
            else           GridChanger.ReconnectNode(_game, pathfindingGrid, x, y);
        }

        _game.ReplacePathfindingGrid(pathfindingGrid);
    }
}