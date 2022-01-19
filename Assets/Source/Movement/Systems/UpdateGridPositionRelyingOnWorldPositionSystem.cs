using System.Collections.Generic;
using Entitas;

public class UpdateGridPositionRelyingOnWorldPositionSystem : ReactiveSystem<GameEntity>
{
    public UpdateGridPositionRelyingOnWorldPositionSystem(Contexts contexts) : base(contexts.game)
    {
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.WorldPosition.Added());
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.hasWorldPosition;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        foreach (var e in entities)
        {
            var newGridPosition = e.worldPosition.value.ToVector2XZInt();

            if (!e.hasGridPosition || e.gridPosition.value != newGridPosition)
            {
                e.ReplaceGridPosition(newGridPosition);
            }
        }
    }
}