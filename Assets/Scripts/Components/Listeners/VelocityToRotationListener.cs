using Entitas;
using UnityEngine;

public class VelocityToRotationListener : MonoBehaviour, IEventListener, IVelocityListener
{
    private GameEntity _entity;

    private void Start()
    {
        if (_entity.hasVelocity) OnVelocity(_entity, _entity.velocity.value);
    }

    public void RegisterEventListeners(IEntity entity)
    {
        _entity = (GameEntity)entity;
        _entity.AddVelocityListener(this);

    }

    public void UnregisterEventListeners()
    {
        _entity.RemoveVelocityListener(this, false);
    }

    public void OnVelocity(GameEntity entity, Vector3 value)
    {
        transform.rotation = Quaternion.LookRotation(value);
    }
}