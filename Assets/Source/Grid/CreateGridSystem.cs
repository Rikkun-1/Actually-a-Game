using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entitas;
using Entitas.Unity;

public class CreateGridSystem : ReactiveSystem<GameEntity>, IInitializeSystem
{
    readonly Contexts contexts;
    IGroup<GameEntity> entitiesOnMap;

    Vector2Int mapSize;

    public CreateGridSystem(Contexts contexts) : base(contexts.game)
    {
        this.contexts = contexts;
        this.entitiesOnMap = this.contexts.game.GetGroup(GameMatcher.Position);
    }

    public void Initialize()
    {
        var e = this.contexts.game.CreateEntity();
        e.AddMap(mapSize);
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.Map.Added());
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.hasMap;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        foreach(var e in this.entitiesOnMap)
        {
            e.isDestroyed = true;
        }

        int sizeX = entities.SingleEntity().map.mapSize.x;
        int sizeY = entities.SingleEntity().map.mapSize.y;

        for (int x = 0; x < sizeX; x++)
        {
            for (int y = 0; y < sizeY; y++)
            {
                var e = contexts.game.CreateEntity();
                e.AddPosition(new Vector2Int(x, y));
                e.AddViewPrefab("cell");
            }
        }
    }
}