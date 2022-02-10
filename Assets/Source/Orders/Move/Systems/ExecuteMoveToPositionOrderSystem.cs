using System.Collections.Generic;
using Entitas;

public class ExecuteMoveToPositionOrderSystem : ReactiveSystem<GameEntity>
{
    public ExecuteMoveToPositionOrderSystem(Contexts contexts) : base(contexts.game)
    {
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.MoveToPositionOrder);
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.hasMoveToPositionOrder;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        foreach (var e in entities)
        {
            var start = e.worldPosition.value.ToVector2XZInt();
            var end   = e.moveToPositionOrder.position;
            e.ReplacePathRequest(start, end);
        }
    }
}