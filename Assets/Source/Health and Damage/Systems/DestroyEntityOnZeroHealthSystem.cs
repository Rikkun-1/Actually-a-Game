using System.Collections.Generic;
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
        foreach (var e in entities)
        {
            if (e.health.value <= 0)
            {
                if (e.unityView.gameObject.GetComponent<RagdollControl>())
                {
                    e.unityView.gameObject.GetComponent<RagdollControl>().MakePhysical();
                    e.RemoveVision();
                    e.RemoveTraversalSpeed();
                    e.hasAI  = false;
                    e.isDead = true;
                }
                else
                {
                    e.isDestroyed = true;
                }
            }
        }
    }
}