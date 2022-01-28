using System.Collections.Generic;
using Entitas;
using UnityEngine;

public class DeleteEntitiesOutsideMapSystem : ReactiveSystem<GameEntity>
{
    private readonly Contexts _contexts;

    public DeleteEntitiesOutsideMapSystem(Contexts contexts) : base(contexts.game)
    {
        _contexts = contexts;
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.GridPosition);
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.hasGridPosition;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        var mapSize = _contexts.game.mapSize.value;

        foreach (var e in entities)
        {
            var position = e.gridPosition.value;

            if (IsOutsideMap(position, mapSize))
            {
                e.isDestroyed = true;
            }
        }
    }

    private bool IsOutsideMap(Vector2Int position, Vector2Int mapSize)
    {
        return position.x < 0 ||
               position.y < 0 ||
               position.x >= mapSize.x ||
               position.y >= mapSize.y;
    }
}