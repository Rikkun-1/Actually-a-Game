using System.Collections.Generic;
using Entitas;
using ProceduralToolkit;

public class VisualizeLookAtDirectionOrderSystem : VisualizeOrderSystemBase, IInitializeSystem
{
    private readonly GameContext _game;

    public VisualizeLookAtDirectionOrderSystem(Contexts contexts) : base(contexts.game)
    {
        _game = contexts.game;
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.LookAtDirectionOrder.AddedOrRemoved());
    }

    protected override bool Filter(GameEntity entity)
    {
        return true;
    }

    public void Initialize()
    {
        visualizationPrefab = _game.gameSettings.value.orderVisualizationPrefabs.lookAtDirectionOrder;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        foreach (var e in entities)
        {
            DestroyVisualizationInstance(e);

            if (!e.hasLookAtDirectionOrder) continue;
            
            var position = e.worldPosition.value;
            var offset   = e.lookAtDirectionOrder.direction.ToVector3XZ();

            var visualizationInstance = CreateVisualizationInstance(e, position + offset);
            
            var viewTransform = e.unityView.gameObject.transform;
            OrderVisualizationHelper.SetupAimConstraint(visualizationInstance, viewTransform);
            OrderVisualizationHelper.SetupPositionConstraint(visualizationInstance, viewTransform, offset);
            OrderVisualizationHelper.PaintInTeamColor(visualizationInstance, e);
        }
    }
}