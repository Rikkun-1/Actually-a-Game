using System.Collections.Generic;
using Entitas;

public class VisualizeShootAtEntityOrderSystem : VisualizeOrderSystemBase, IInitializeSystem
{
    private readonly GameContext                        _game;

    public VisualizeShootAtEntityOrderSystem(Contexts contexts) : base(contexts.game)
    {
        _game = contexts.game;
    }

    protected override ICollector<GameEntity> GetTrigger(
        IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.ShootAtEntityOrder.AddedOrRemoved());
    }

    protected override bool Filter(GameEntity entity)
    {
        return true;
    }

    public void Initialize()
    {
        visualizationPrefab = _game.gameSettings.value.orderVisualizationPrefabs.shootAtEntityOrder;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        foreach (var e in entities)
        {
            DestroyVisualizationInstance(e);
            if (!e.hasShootAtEntityOrder) continue;

            var entityWithId = _game.GetEntityWithId(e.shootAtEntityOrder.targetID);
            if (entityWithId == null) return;

            var visualizationInstance = CreateVisualizationInstance(e);
            
            var transform = entityWithId.unityView.gameObject.transform;
            OrderVisualizationHelper.SetupPositionConstraint(visualizationInstance, transform);
            OrderVisualizationHelper.PaintInTeamColor(visualizationInstance, e);
        }
    }
}