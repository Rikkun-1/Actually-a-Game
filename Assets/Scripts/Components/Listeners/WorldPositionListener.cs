using Entitas;
using UnityEngine;

public class WorldPositionListener : MonoBehaviour, IEventListener, IWorldPositionListener
{
    private GameEntity _entity;

    private void Start()
    {
        if (_entity.hasWorldPosition) OnWorldPosition(_entity, _entity.worldPosition.value);
    }
    
    public void RegisterEventListeners(IEntity entity)
    {
        _entity = (GameEntity)entity;
        _entity.AddWorldPositionListener(this);
    }

    public void UnregisterEventListeners()
    {
        _entity.RemoveWorldPositionListener(this, false);
    }

    public void OnWorldPosition(GameEntity e, Vector3 newPosition)
    {
        transform.position = newPosition;
    }
}