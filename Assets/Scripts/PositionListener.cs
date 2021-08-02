using UnityEngine;
using Entitas;

public class PositionListener : MonoBehaviour, IEventListener, IPositionListener
{
    GameEntity entity;

    public void RegisterEventListeners(IEntity entity)
    {
        this.entity = (GameEntity)entity;
        this.entity.AddPositionListener(this);
    }

    public void OnPosition(GameEntity e, Vector2Int newPosition)
    {
        transform.position = new Vector3(newPosition.x, newPosition.y);
    }
}