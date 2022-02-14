using UnityEngine;

[AddComponentMenu("")] // hide in component menu
public abstract class BaseVisionListener : BaseEventListener, IVisionListener
{
    public virtual void OnVision(GameEntity entity, float directionAngle, int viewingAngle, int distance, int turningSpeed) {}
    
    protected override void Register()                 => gameEntity.AddVisionListener(this);
    public override    void UnregisterEventListeners() => gameEntity.RemoveVisionListener(this, false);
}