using System.Collections.Generic;
using Entitas;
using UnityEngine;

public class CreateGridSystem : ReactiveSystem<GameEntity>, IInitializeSystem
{
    private readonly Contexts _contexts;

    private readonly Vector2Int _defaultMapSize = new Vector2Int(8, 8);

    private readonly IGroup<GameEntity> _entitiesOnMap;

    public CreateGridSystem(Contexts contexts) : base(contexts.game)
    {
        _contexts = contexts;
        _entitiesOnMap = _contexts.game.GetGroup(GameMatcher.Position);
    }

    public void Initialize()
    {
        var e = _contexts.game.CreateEntity();
        e.AddMapSize(_defaultMapSize);
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

        var mapSize = entities.SingleEntity().mapSize.Value;
        var sizeX = mapSize.x;
        var sizeY = mapSize.y;

        for (var x = 0; x < sizeX; x++)
        {
            for (var y = 0; y < sizeY; y++)
            {
                var e = _contexts.game.CreateEntity();
                e.AddPosition(new Vector2Int(x, y));
                e.AddViewPrefab("floor");
            }
        }
    }

    private void DestroyMap()
    {
        foreach (var e in _entitiesOnMap)
        {
            e.isDestroyed = true;
        }
    }
}