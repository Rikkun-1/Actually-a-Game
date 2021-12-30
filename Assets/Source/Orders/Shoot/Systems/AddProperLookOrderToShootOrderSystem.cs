using System.Collections.Generic;
using Entitas;

public class AddProperLookOrderToShootOrderSystem : ReactiveSystem<GameEntity>
{
    public AddProperLookOrderToShootOrderSystem(Contexts contexts) : base(contexts.game)
    {
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.ShootAtDirectionOrder.Added(),
                                       GameMatcher.ShootAtPositionOrder.Added(),
                                       GameMatcher.ShootAtEntityOrder.Added());
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.hasShootAtDirectionOrder ||
               entity.hasShootAtPositionOrder ||
               entity.hasShootAtEntityOrder;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        foreach (var e in entities)
        {
            if (e.hasShootAtDirectionOrder) e.ReplaceLookAtDirectionOrder(e.shootAtDirectionOrder.angle);
            if (e.hasShootAtPositionOrder)  e.ReplaceLookAtPositionOrder(e.shootAtPositionOrder.position);
            if (e.hasShootAtEntityOrder)    e.ReplaceLookAtEntityOrder(e.shootAtEntityOrder.targetID);
        }
    }
}