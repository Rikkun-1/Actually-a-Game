using System.Collections.Generic;
using Entitas;
using UnityEngine;

public class UpdateGridSizeSystem : ReactiveSystem<GameEntity>
{
    private readonly Contexts           _contexts;
    private readonly IGroup<GameEntity> _entitiesOnMap;

    public UpdateGridSizeSystem(Contexts contexts) : base(contexts.game)
    {
        _contexts      = contexts;
        _entitiesOnMap = _contexts.game.GetGroup(GameMatcher.GridPosition);
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
        DestroyMap();
        PopulateMapWithFloor(_contexts.game.mapSize.value);
    }

    private void DestroyMap()
    {
        foreach (var e in _entitiesOnMap)
        {
            e.isDestroyed = true;
        }
    }

    private void PopulateMapWithFloor(Vector2Int size)
    {
        for (var x = 0; x < size.x; x++)
        {
            for (var y = 0; y < size.y; y++)
            {
                var e = EntityCreator.CreateGameEntity();
                e.AddWorldPosition(new Vector2(x, y));
                e.AddViewPrefab("floor");
            }
        }
    }
}