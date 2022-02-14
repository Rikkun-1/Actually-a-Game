using System.Collections.Generic;
using Entitas;

public class RemoveLookOrdersWhenDestroyedSystem : ReactiveSystem<GameEntity>
{
    public RemoveLookOrdersWhenDestroyedSystem(Contexts contexts) : base(contexts.game)
    {
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.Destroyed);
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.isDestroyed;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        foreach (var e in entities)
        {
            if(e.hasLookAtDirectionOrder) e.RemoveLookAtDirectionOrder();
            if(e.hasLookAtPositionOrder) e.RemoveLookAtPositionOrder();
            if(e.hasLookAtEntityOrder) e.RemoveLookAtEntityOrder();
        }
    }
}