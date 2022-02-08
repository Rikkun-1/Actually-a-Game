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
        return entity.hasLookOrder;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        foreach (var e in entities)
        {
            e.RemoveLookOrder();
        }
    }
}