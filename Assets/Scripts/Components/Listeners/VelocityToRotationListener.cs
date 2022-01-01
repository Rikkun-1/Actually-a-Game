using Entitas;
using UnityEngine;

public class VelocityToRotationListener : MonoBehaviour, IEventListener, IVelocityListener
{
    private GameEntity _entity;

    public void RegisterEventListeners(IEntity entity)
    {
        _entity = (GameEntity)entity;
        _entity.AddVelocityListener(this);

        if (_entity.hasVelocity)
        {
            var velocity = _entity.velocity.value;
            OnVelocity(_entity, velocity);
        }
    }

    public void UnregisterEventListeners()
    {
        _entity.RemoveVelocityListener(this, false);
    }

    public void OnVelocity(GameEntity entity, Vector2 value)
    {
        transform.rotation = Quaternion.Euler(0, value.ToAngle(), 0);
    }
}