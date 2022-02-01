using System.Collections.Generic;
using System.Linq;
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
        return entity.hasHealth && !entity.isIndestructible && !entity.isDead;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        var entitiesWithZeroHealth = entities.Where(e => e.health.currentHealth <= 0);
        
        foreach (var e in entitiesWithZeroHealth)
        {
            if (e.hasVision)         e.RemoveVision();
            if (e.hasTraversalSpeed) e.RemoveTraversalSpeed();
            e.hasAI  = false;
            e.isDead = true;
            
            if (e.hasUnityView)
            {
                var destroyableComponent = e.unityView.gameObject.GetComponent<Destroyable>();
                if (destroyableComponent != null)
                {
                    destroyableComponent.OnDestroy.Invoke();
                    continue;
                }
            } 
            
            e.isDestroyed = true;
        }
    }
}