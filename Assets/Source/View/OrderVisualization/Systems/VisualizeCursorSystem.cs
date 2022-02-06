using System.Collections.Generic;
using Entitas;
using UnityEngine;
using Object = UnityEngine.Object;

public class VisualizeCursorSystem : ReactiveSystem<InputEntity>, IInitializeSystem
{
    private readonly InputContext _input;
    private readonly GameContext  _game;
    private          GameObject   _visualizationPrefab;
    private          GameObject   _visualizationInstance;

    public VisualizeCursorSystem(Contexts contexts) : base(contexts.input)
    {
        _input = contexts.input;
        _game  = contexts.game;
    }

    protected override ICollector<InputEntity> GetTrigger(IContext<InputEntity> context)
    {
        return context.CreateCollector(InputMatcher.MousePosition);
    }

    protected override bool Filter(InputEntity entity)
    {
        return entity.hasMousePosition;
    }

    public void Initialize()
    {
        _visualizationPrefab   = _game.gameSettings.value.orderVisualizationPrefabs.gridCursor;
        _visualizationInstance = Object.Instantiate(_visualizationPrefab);
        
        OrderVisualizationHelper.PaintSpriteRenderer(_visualizationInstance, Color.green);
    }

    protected override void Execute(List<InputEntity> entities)
    {
        _visualizationInstance.transform.position = _input.mousePosition.position.ToVector3XZInt();
    }
}