using System.Collections.Generic;
using Entitas;

public class VisualizeViewDestructionSystem : ReactiveSystem<GameEntity>
{
    public VisualizeViewDestructionSystem(Contexts contexts) : base(contexts.game)
    {
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.Destroyed);
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.hasUnityView;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        foreach (var e in entities)
        {
            var destroyableComponent = e.unityView.gameObject.GetComponent<Destroyable>();
            if (destroyableComponent == null) continue;
                
            destroyableComponent.OnDestroy.Invoke();
        }
    }
}