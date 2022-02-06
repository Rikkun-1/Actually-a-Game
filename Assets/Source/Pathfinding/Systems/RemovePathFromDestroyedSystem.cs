using System.Collections.Generic;
using Entitas;

public class RemovePathFromDestroyedSystem : ReactiveSystem<GameEntity>
{
    public RemovePathFromDestroyedSystem(Contexts contexts) : base(contexts.game)
    {
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.Destroyed);
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.isDestroyed && entity.hasPath;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        foreach (var e in entities)
        {
            e.RemovePath();
        }
    }
}