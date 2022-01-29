using UnityEngine;

[AddComponentMenu("")] // hide in component menu
public abstract class BaseVelocityListener : BaseEventListener, IVelocityListener
{
    public abstract void OnVelocity(GameEntity entity, Vector3 newVelocity);
    
    protected override void Register()                 => gameEntity.AddVelocityListener(this);
    public override    void UnregisterEventListeners() => gameEntity.RemoveVelocityListener(this, false);
}