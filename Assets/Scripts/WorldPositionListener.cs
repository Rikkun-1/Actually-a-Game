using Entitas;
using UnityEngine;

public class WorldPositionListener : MonoBehaviour, IEventListener, IWorldPositionListener
{
    private GameEntity _entity;

    public void RegisterEventListeners(IEntity entity)
    {
        _entity = (GameEntity)entity;
        _entity.AddWorldPositionListener(this);

        if (_entity.hasWorldPosition)
        {
            var currentPosition = _entity.worldPosition.value;
            OnWorldPosition(_entity, currentPosition.ToVector2Int());
        }
    }

    public void UnregisterEventListeners()
    {
        _entity.RemoveWorldPositionListener(this, false);
    }

    public void OnWorldPosition(GameEntity e, Vector2 newPosition)
    {
        transform.localPosition = new Vector3(newPosition.x, 0, newPosition.y);
    }
}