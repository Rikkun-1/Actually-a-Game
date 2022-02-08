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
        return entity.hasShootOrder;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        foreach (var e in entities)
        {
            e.RemoveShootOrder();
        }
    }
}