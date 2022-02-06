using System.Collections.Generic;
using Entitas;

public class RemoveShootOrdersFromDestroyedSystem : ReactiveSystem<GameEntity>
{
    public RemoveShootOrdersFromDestroyedSystem(Contexts contexts) : base(contexts.game)
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
            if(e.hasShootAtDirectionOrder) e.RemoveShootAtDirectionOrder();
            if(e.hasShootAtPositionOrder) e.RemoveShootAtPositionOrder();
            if(e.hasShootAtEntityOrder) e.RemoveShootAtEntityOrder();
        }
    }
}