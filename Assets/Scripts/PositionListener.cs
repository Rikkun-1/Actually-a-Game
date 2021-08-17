using Entitas;

using UnityEngine;

public class PositionListener : MonoBehaviour, IEventListener, IPositionListener
{
    private GameEntity _entity;

    public void RegisterEventListeners(IEntity entity)
    {
        _entity = (GameEntity)entity;
        _entity.AddPositionListener(this);

        var position = _entity.position.Value;
        OnPosition(_entity, new Vector2Int(position.x, position.y));
    }

    public void UnregisterEventListeners()
    {
        _entity.RemovePositionListener(this, removeComponentWhenEmpty: false);
    }

    public void OnPosition(GameEntity e, Vector2Int newPosition)
    {
        transform.localPosition = new Vector3(newPosition.x, newPosition.y);
    }
}