using System.Collections.Generic;
using Entitas;
using UnityEngine;

public class DeleteEntitiesOutsideGridSystem : ReactiveSystem<GameEntity>
{
    private readonly GameContext _game;

    public DeleteEntitiesOutsideGridSystem(Contexts contexts) : base(contexts.game)
    {
        _game = contexts.game;
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
        var gridSize = _game.gridSize.value;

        foreach (var e in entities)
        {
            var position = e.gridPosition.value;

            if (IsOutsideMap(position, gridSize))
            {
                e.isDestroyed = true;
            }
        }
    }

    private static bool IsOutsideMap(Vector2Int position, Vector2Int gridSize)
    {
        return position.x < 0 ||
               position.y < 0 ||
               position.x >= gridSize.x ||
               position.y >= gridSize.y;
    }
}