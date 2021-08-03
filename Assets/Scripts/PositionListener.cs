using UnityEngine;
using Entitas;

public class PositionListener : MonoBehaviour, IEventListener, IPositionListener
{
    GameEntity entity;

    public void RegisterEventListeners(IEntity entity)
    {
        this.entity = (GameEntity)entity;
        this.entity.AddPositionListener(this);

        var position = this.entity.position.value;
        this.OnPosition(this.entity, new Vector2Int(position.x, position.y));
    }

    public void UnregisterEventListeners()
    {
        this.entity.RemovePositionListener(this, removeComponentWhenEmpty: false);
    }

    public void OnPosition(GameEntity e, Vector2Int newPosition)
    {
        this.transform.position = new Vector3(newPosition.x, newPosition.y);
    }
}