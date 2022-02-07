﻿using System.Collections.Generic;
using Entitas;

public class DestroyEntityOnZeroHealthSystem : ReactiveSystem<GameEntity>
{
    public DestroyEntityOnZeroHealthSystem(Contexts contexts) : base(contexts.game)
    {
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.Health.Added());
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.hasHealth && !entity.isIndestructible && !entity.isDestroyed;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        foreach (var e in entities)
        {
            if(e.health.currentHealth > 0) return;
            
            if (e.hasVision)         e.RemoveVision();
            if (e.hasTraversalSpeed) e.RemoveTraversalSpeed();
            e.hasAI       = false;
            e.isDestroyed = true;

            if (!e.hasUnityView) continue;
            var destroyableComponent = e.unityView.gameObject.GetComponent<Destroyable>();
            if (destroyableComponent == null) continue;
                
            destroyableComponent.OnDestroy.Invoke();
        }
    }
}