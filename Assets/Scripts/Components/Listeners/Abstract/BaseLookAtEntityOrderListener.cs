using UnityEngine;

[AddComponentMenu("")] // hide in component menu
public abstract class BaseLookAtEntityOrderListener : BaseEventListener, ILookAtEntityOrderListener
{
    public virtual void OnLookAtEntityOrder(GameEntity entity, long targetID) {}
    
    protected override void Register()                 => gameEntity.AddLookAtEntityOrderListener(this);
    public override    void UnregisterEventListeners() => gameEntity.RemoveLookAtEntityOrderListener(this, false);
}