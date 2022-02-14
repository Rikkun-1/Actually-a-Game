using System.Collections.Generic;
using Entitas;

public class RemoveReactionStartTimeWhenShootAtEntityOrderRemovedSystem : ReactiveSystem<GameEntity>
{
    public RemoveReactionStartTimeWhenShootAtEntityOrderRemovedSystem(Contexts contexts) : base(contexts.game)
    {
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.ShootOrder.Removed());
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