using System.Collections.Generic;
using Entitas;
using UnityEngine;
using UnityEngine.Animations;

public class VisualizeSelectedEntitySystem : ReactiveSystem<InputEntity>, IInitializeSystem
{
    private readonly InputContext _input;
    private readonly GameContext  _game;

    private GameObject _selectedEntityVisualizationPrefab;
    private GameObject _selectedEntityVisualizationInstance;

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
        _selectedEntityVisualizationPrefab = Resources.Load<GameObject>("Prefabs/SelectedVisualization");
    }

    protected override void Execute(List<InputEntity> entities)
    {
        Object.Destroy(_selectedEntityVisualizationInstance);

        if (!_input.hasSelectedEntity) return;
        var selectedGameEntity = _game.GetEntityWithId(_input.selectedEntity.gameEntityID);
        if (!(selectedGameEntity is { hasUnityView: true })) return;
        
        _selectedEntityVisualizationInstance = Object.Instantiate(_selectedEntityVisualizationPrefab);

        var view = selectedGameEntity.unityView.gameObject;

        var constraintSource = new ConstraintSource
        {
            sourceTransform = view.transform, 
            weight = 1
        };
        var positionConstraint = _selectedEntityVisualizationInstance.GetComponent<PositionConstraint>();
        positionConstraint.AddSource(constraintSource);
        positionConstraint.constraintActive = true;
    }
}