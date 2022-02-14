using System.Collections.Generic;
using Entitas;

public class ProcessCalculateVelocityByPositionChangesRequestsSystem : ReactiveSystem<GameEntity>
{
    public ProcessCalculateVelocityByPositionChangesRequestsSystem(Contexts contexts) : base(contexts.game)
    {
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.CalculateVelocityByPositionChanges.AddedOrRemoved());
    }

    protected override bool Filter(GameEntity entity)
    {
        return true;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        foreach (var e in entities)
        {
            e.enablePreviousWorldPositionMemorization = e.enableCalculateVelocityByPositionChanges;
            e.isIgnoredByMoveSystem                   = e.enableCalculateVelocityByPositionChanges;
        }
    }
}