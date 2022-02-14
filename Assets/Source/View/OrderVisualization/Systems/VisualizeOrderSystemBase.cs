using System.Collections.Generic;
using Entitas;
using UnityEngine;

public abstract class VisualizeOrderSystemBase : ReactiveSystem<GameEntity>
{
    private readonly Dictionary<GameEntity, GameObject> _visualizationInstances = new Dictionary<GameEntity, GameObject>();

    private Transform _parent;

    protected VisualizeOrderSystemBase(IContext<GameEntity> context) : base(context)
    {
    }
    
    protected GameObject CreateVisualizationInstance(GameEntity e, GameObject prefab, Vector3 position = new Vector3())
    {
        _parent ??= new GameObject(prefab.name).transform;

        var gameObject = Object.Instantiate(prefab, position, prefab.transform.rotation, _parent);
        _visualizationInstances[e] = gameObject;
        return gameObject;
    }
    
    protected void DestroyVisualizationInstance(GameEntity e)
    {
        _visualizationInstances.TryGetValue(e, out var gameObject);
        Object.Destroy(gameObject);
    }
}