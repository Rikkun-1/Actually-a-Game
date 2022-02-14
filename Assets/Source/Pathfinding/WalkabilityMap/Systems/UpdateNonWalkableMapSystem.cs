using System.Collections.Generic;
using Entitas;

public class UpdateNonWalkableMapSystem : ReactiveSystem<GameEntity>
{
    private readonly GameContext _game;

    public UpdateNonWalkableMapSystem(Contexts contexts) : base(contexts.game)
    {
        _game = contexts.game;
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.NonWalkable.AddedOrRemoved());
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

            if (e.isNonWalkable) GridChanger.DisconnectNode(pathfindingGrid, x, y);
            else                 GridChanger.ReconnectNode(_game, pathfindingGrid, x, y);
        }

        _game.ReplacePathfindingGrid(pathfindingGrid);
    }
}