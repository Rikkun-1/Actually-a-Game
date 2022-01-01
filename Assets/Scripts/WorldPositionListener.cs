using Entitas;
using UnityEngine;

public class WorldPositionListener : MonoBehaviour, IEventListener, IWorldPositionListener
{
    private GameEntity _entity;

    public void RegisterEventListeners(IEntity entity)
    {
        _entity = (GameEntity)entity;
        _entity.AddWorldPositionListener(this);

        var currentPosition = _entity.gridPosition.value;
        OnWorldPosition(_entity, new Vector2Int(currentPosition.x, currentPosition.y));
    }

    public void UnregisterEventListeners()
    {
        _entity.RemoveWorldPositionListener(this, false);
    }

    public void OnWorldPosition(GameEntity e, Vector2 newPosition)
    {
        transform.localPosition = new Vector3(newPosition.x, newPosition.y);
    }
}