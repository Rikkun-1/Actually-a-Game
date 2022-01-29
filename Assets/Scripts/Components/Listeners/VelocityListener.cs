using UnityEngine;

public abstract class VelocityListener : EventListener, IVelocityListener
{
    public abstract void OnVelocity(GameEntity entity, Vector3 newVelocity);
    
    protected override void Register()                 => gameEntity.AddVelocityListener(this);
    public override    void UnregisterEventListeners() => gameEntity.RemoveVelocityListener(this, false);
}