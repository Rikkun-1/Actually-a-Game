using UnityEngine;

public class VelocityToRotationListener : EventListener, IVelocityListener
{
    public void OnVelocity(GameEntity entity, Vector3 value)
    {
        transform.rotation = Quaternion.LookRotation(value);
    }
    
    protected override void Register()                 => gameEntity.AddVelocityListener(this);
    public override    void UnregisterEventListeners() => gameEntity.RemoveVelocityListener(this, false);
}