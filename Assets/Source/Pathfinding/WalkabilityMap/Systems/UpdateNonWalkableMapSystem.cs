using System.Collections.Generic;
using Entitas;

public class UpdateNonWalkableMapSystem : ReactiveSystem<GameEntity>
{
    private readonly Contexts _contexts;

    public UpdateNonWalkableMapSystem(Contexts contexts) : base(contexts.game)
    {
        _contexts = contexts;
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.NonWalkable.AddedOrRemoved(),
                                       GameMatcher.NorthWall.AddedOrRemoved(),
                                       GameMatcher.SouthWall.AddedOrRemoved(),
                                       GameMatcher.EastWall.AddedOrRemoved(),
                                       GameMatcher.WestWall.AddedOrRemoved());
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.hasGridPosition;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        var pathfindingGrid = _contexts.game.pathfindingGrid.value;

        foreach (var e in entities)
        {
            var x = e.gridPosition.value.x;
            var y = e.gridPosition.value.y;

            if (e.isNonWalkable)    GridChanger.DisconnectNode(pathfindingGrid, x, y);
            else if (e.isEastWall)  GridChanger.WallAdded(pathfindingGrid, x, y, Direction.Right);
            else if (e.isWestWall)  GridChanger.WallAdded(pathfindingGrid, x, y, Direction.Left);
            else if (e.isNorthWall) GridChanger.WallAdded(pathfindingGrid, x, y, Direction.Top);
            else if (e.isSouthWall) GridChanger.WallAdded(pathfindingGrid, x, y, Direction.Bottom);
            else                    GridChanger.ReconnectNode(_contexts.game, pathfindingGrid, x, y);
        }

        _contexts.game.ReplacePathfindingGrid(pathfindingGrid);
    }
}