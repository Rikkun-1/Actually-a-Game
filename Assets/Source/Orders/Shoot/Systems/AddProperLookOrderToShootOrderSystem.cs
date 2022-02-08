using System.Collections.Generic;
using Entitas;

public class AddProperLookOrderToShootOrderSystem : ReactiveSystem<GameEntity>
{
    public AddProperLookOrderToShootOrderSystem(Contexts contexts) : base(contexts.game)
    {
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.ShootOrder);
    }

    protected override bool Filter(GameEntity entity)
    {
        return true;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        foreach (var e in entities)
        {
            e.ReplaceLookOrder(e.shootOrder.target);
        }
    }
}