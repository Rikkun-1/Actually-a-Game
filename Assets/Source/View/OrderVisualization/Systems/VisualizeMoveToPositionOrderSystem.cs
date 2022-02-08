using System.Collections.Generic;
using Entitas;
using UnityEngine;

public class VisualizeMoveToPositionOrderSystem : VisualizeOrderSystemBase, IInitializeSystem
{
    private readonly GameContext _game;

    private GameObject _visualizationPrefab;

    public VisualizeMoveToPositionOrderSystem(Contexts contexts) : base(contexts.game)
    {
        _game = contexts.game;
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.MoveToPositionOrder.AddedOrRemoved());
    }

    protected override bool Filter(GameEntity entity)
    {
        return true;
    }

    public void Initialize()
    {
        _visualizationPrefab = _game.gameSettings.value.orderVisualizationPrefabs.moveToPositionOrder;
    }
    
    protected override void Execute(List<GameEntity> entities)
    {
        foreach (var e in entities)
        {
            DestroyVisualizationInstance(e);
            if (!e.hasMoveToPositionOrder) continue;

            var position              = e.moveToPositionOrder.position.ToVector3XZ();
            var visualizationInstance = CreateVisualizationInstance(e, _visualizationPrefab, position);
            
            OrderVisualizationHelper.PaintInTeamColor(visualizationInstance, e);
        }
    }
}