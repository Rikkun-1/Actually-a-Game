using System.Collections.Generic;
using Entitas;
using ProceduralToolkit;

public class VisualizeLookAtPositionOrderSystem : VisualizeOrderSystemBase, IInitializeSystem
{
    private readonly GameContext _game;
    
    public VisualizeLookAtPositionOrderSystem(Contexts contexts) : base(contexts.game)
    {
        _game = contexts.game;
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.LookAtPositionOrder.AddedOrRemoved());
    }

    protected override bool Filter(GameEntity entity)
    {
        return true;
    }

    public void Initialize()
    {
        visualizationPrefab = _game.gameSettings.value.orderVisualizationPrefabs.lookAtPositionOrder;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        foreach (var e in entities)
        {
            DestroyVisualizationInstance(e);
            if (!e.hasLookAtPositionOrder) continue;

            var position              = e.lookAtPositionOrder.position.ToVector3XZ();
            var visualizationInstance = CreateVisualizationInstance(e, position);
            
            OrderVisualizationHelper.PaintInTeamColor(visualizationInstance, e);
        }
    }
}