using System.Collections.Generic;
using Entitas;
using UnityEngine;

public class VisualizeShootAtEntityOrderSystem : VisualizeOrderSystemBase, IInitializeSystem
{
    private readonly GameContext _game;
    
    private GameObject _visualizationPrefab;

    public VisualizeShootAtEntityOrderSystem(Contexts contexts) : base(contexts.game)
    {
        _game = contexts.game;
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.ShootOrder.AddedOrRemoved());
    }

    protected override bool Filter(GameEntity entity)
    {
        return true;
    }

    public void Initialize()
    {
        _visualizationPrefab = _game.gameSettings.value.orderVisualizationPrefabs.shootAtEntityOrder;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        foreach (var e in entities)
        {
            DestroyVisualizationInstance(e);
            if (!e.hasShootOrder || e.shootOrder.target.targetType != TargetType.Entity || e.teamID.value != 0) continue;

            var entityWithId = _game.GetEntityWithId(e.shootOrder.target.entityID);
            if (!(entityWithId is { hasUnityView: true })) return;

            var visualizationInstance = CreateVisualizationInstance(e, _visualizationPrefab);
            
            var transform = entityWithId.unityView.gameObject.transform;
            OrderVisualizationHelper.SetupPositionConstraint(visualizationInstance, transform);
            OrderVisualizationHelper.PaintInTeamColor(visualizationInstance, e);
        }
    }
}