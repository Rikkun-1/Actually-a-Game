using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entitas;

public class RemoveReactionStartTimeWhenShootAtEntityOrderRemovedSystem : ReactiveSystem<GameEntity>
{
    private readonly Contexts _contexts;

    public RemoveReactionStartTimeWhenShootAtEntityOrderRemovedSystem(Contexts contexts) : base(contexts.game)
    {
        _contexts = contexts;
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.ShootAtEntityOrder.Removed());
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.hasReactionStartTime;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        foreach (var e in entities)
        {
            e.RemoveReactionStartTime();
        }
    }
}