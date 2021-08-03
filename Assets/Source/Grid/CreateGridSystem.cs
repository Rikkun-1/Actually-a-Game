using System.Collections.Generic;
using UnityEngine;
using Entitas;

public class CreateGridSystem : ReactiveSystem<GameEntity>, IInitializeSystem
{
    readonly Contexts contexts;

    readonly IGroup<GameEntity> entitiesOnMap;

    Vector2Int defaultMapSize = new Vector2Int(5, 5);

    public CreateGridSystem(Contexts contexts) : base(contexts.game)
    {
        this.contexts = contexts;
        this.entitiesOnMap = this.contexts.game.GetGroup(GameMatcher.Position);
    }

    public void Initialize()
    {
        var e = this.contexts.game.CreateEntity();
        e.AddMapSize(this.defaultMapSize);
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
        this.destroyMap();

        var mapSize = entities.SingleEntity().mapSize.value;
        int sizeX = mapSize.x;
        int sizeY = mapSize.y;

        for (int x = 0; x < sizeX; x++)
        {
            for (int y = 0; y < sizeY; y++)
            {
                var e = this.contexts.game.CreateEntity();
                e.AddPosition(new Vector2Int(x, y));
                e.AddViewPrefab("floor");
            }
        }
    }

    void destroyMap()
    {
        foreach (var e in this.entitiesOnMap)
        {
            e.isDestroyed = true;
        }
    }
}