using Entitas;

using System.Collections.Generic;

using UnityEngine;

public class CreateGridSystem : ReactiveSystem<GameEntity>, IInitializeSystem
{
    private readonly Contexts _contexts;

    private readonly IGroup<GameEntity> _entitiesOnMap;

    private Vector2Int _defaultMapSize = new Vector2Int(8, 8);

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
        int sizeX = mapSize.x;
        int sizeY = mapSize.y;

        for (int x = 0; x < sizeX; x++)
        {
            for (int y = 0; y < sizeY; y++)
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