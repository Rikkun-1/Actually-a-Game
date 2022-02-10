using System.Collections.Generic;
using Entitas;
using UnityEngine;

public class UpdateGridSizeSystem : ReactiveSystem<GameEntity>
{
    private readonly IGroup<GameEntity> _entitiesOnMap;
    private readonly GameContext        _game;

    public UpdateGridSizeSystem(Contexts contexts) : base(contexts.game)
    {
        _game          = contexts.game;
        _entitiesOnMap = _game.GetGroup(GameMatcher.GridPosition);
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
        DestroyMap();
        PopulateMapWithFloor(_game.gridSize.value);
    }

    private void DestroyMap()
    {
        foreach (var e in _entitiesOnMap.GetEntities())
        {
            e.isDestroyed = true;
        }
    }

    private static void PopulateMapWithFloor(Vector2Int size)
    {
        for (var x = 0; x < size.x; x++)
        {
            for (var z = 0; z < size.y; z++)
            {
                var e = EntityCreator.CreateGameEntity();
                e.AddWorldPosition(new Vector3(x, 0, z));
                e.AddViewPrefab("Prefabs/floor");
            }
        }
    }
}