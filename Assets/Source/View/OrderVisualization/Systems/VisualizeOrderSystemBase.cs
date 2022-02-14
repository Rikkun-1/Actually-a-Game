using System.Collections.Generic;
using Entitas;
using UnityEngine;

public abstract class VisualizeOrderSystemBase : ReactiveSystem<GameEntity>
{
    protected GameObject visualizationPrefab;

    private readonly Dictionary<GameEntity, GameObject> _visualizationInstances = new Dictionary<GameEntity, GameObject>();

    protected VisualizeOrderSystemBase(IContext<GameEntity> context) : base(context)
    {
    }
    
    protected GameObject CreateVisualizationInstance(GameEntity e, Vector3 position = new Vector3())
    {
        var gameObject = Object.Instantiate(visualizationPrefab, position, visualizationPrefab.transform.rotation);
        _visualizationInstances[e] = gameObject;
        return gameObject;
    }
    
    protected void DestroyVisualizationInstance(GameEntity e)
    {
        _visualizationInstances.TryGetValue(e, out var gameObject);
        Object.Destroy(gameObject);
    }
}