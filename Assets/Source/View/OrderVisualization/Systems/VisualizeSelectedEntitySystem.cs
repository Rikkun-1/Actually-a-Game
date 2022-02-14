using System.Collections.Generic;
using Entitas;
using UnityEngine;

public class VisualizeSelectedEntitySystem : ReactiveSystem<InputEntity>, IInitializeSystem
{
    private readonly InputContext _input;
    private readonly GameContext  _game;
    private          GameObject   _visualizationPrefab;
    private          GameObject   _visualizationInstance;

    public VisualizeSelectedEntitySystem(Contexts contexts) : base(contexts.input)
    {
        _input = contexts.input;
        _game  = contexts.game;
    }

    protected override ICollector<InputEntity> GetTrigger(IContext<InputEntity> context)
    {
        return context.CreateCollector(InputMatcher.SelectedEntity.AddedOrRemoved());
    }

    protected override bool Filter(InputEntity entity)
    {
        return true;
    }

    public void Initialize()
    {
        _visualizationPrefab = _game.gameSettings.value.orderVisualizationPrefabs.selectedEntity;
    }

    protected override void Execute(List<InputEntity> entities)
    {
        Object.Destroy(_visualizationInstance);
        if (!_input.hasSelectedEntity) return;
        
        var entityWithId = _game.GetEntityWithId(_input.selectedEntity.gameEntityID);
        if (entityWithId is { hasUnityView: false }) return;
        
        _visualizationInstance = Object.Instantiate(_visualizationPrefab);
        
        var transform = entityWithId.unityView.gameObject.transform;
        OrderVisualizationHelper.SetupPositionConstraint(_visualizationInstance, transform);
        OrderVisualizationHelper.PaintInTeamColor(_visualizationInstance, entityWithId);
    }
}