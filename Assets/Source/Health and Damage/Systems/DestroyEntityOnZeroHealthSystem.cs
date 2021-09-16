using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entitas;

public class DestroyEntityOnZeroHealthSystem : ReactiveSystem<GameEntity>
{
    private readonly Contexts _contexts;

    public DestroyEntityOnZeroHealthSystem(Contexts contexts) : base(contexts.game)
    {
        _contexts = contexts;
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.Health.Added());
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.hasHealth;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        foreach (var e in entities)
        {
            if (e.health.value <= 0)
            {
                e.isDestroyed = true;
            }
        }
    }
}