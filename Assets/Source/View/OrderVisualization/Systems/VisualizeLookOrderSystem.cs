using System;
using System.Collections.Generic;
using Entitas;
using ProceduralToolkit;
using UnityEngine;

public class VisualizeLookOrderSystem : VisualizeOrderSystemBase, IInitializeSystem
{
    private readonly GameContext _game;
    
    private GameObject _lookAtDirectionPrefab;
    private GameObject _lookAtPositionPrefab;

    public VisualizeLookOrderSystem(Contexts contexts) : base(contexts.game)
    {
        _game = contexts.game;
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.LookOrder.AddedOrRemoved());
    }

    protected override bool Filter(GameEntity entity)
    {
        return true;
    }

    public void Initialize()
    {
        _lookAtDirectionPrefab = _game.gameSettings.value.orderVisualizationPrefabs.lookAtDirectionOrder;
        _lookAtPositionPrefab  = _game.gameSettings.value.orderVisualizationPrefabs.lookAtPositionOrder;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        foreach (var e in entities)
        {
            DestroyVisualizationInstance(e);

            if (!e.hasLookOrder) continue;
            
            GameObject visualizationInstance;
            switch (e.lookOrder.target.targetType)
            {
                case TargetType.Direction:
                {
                    var position = e.worldPosition.value;
                    var offset   = e.lookOrder.target.direction.ToVector3XZ();
                    visualizationInstance = CreateVisualizationInstance(e, _lookAtDirectionPrefab, position + offset);

                    var viewTransform = e.unityView.gameObject.transform;
                    OrderVisualizationHelper.SetupAimConstraint(visualizationInstance, viewTransform);
                    OrderVisualizationHelper.SetupPositionConstraint(visualizationInstance, viewTransform, offset);
                    break;
                }
                case TargetType.Position:
                {
                    var position = e.lookOrder.target.position.ToVector3XZ();
                    visualizationInstance = CreateVisualizationInstance(e, _lookAtPositionPrefab, position);
                    break;
                }
                case TargetType.Entity: continue;
                default: throw new ArgumentOutOfRangeException();
            }
            OrderVisualizationHelper.PaintInTeamColor(visualizationInstance, e);
        }
    }
}