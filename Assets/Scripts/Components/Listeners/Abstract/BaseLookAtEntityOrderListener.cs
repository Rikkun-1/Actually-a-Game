using UnityEngine;

[AddComponentMenu("")] // hide in component menu
public abstract class BaseLookAtEntityOrderListener : BaseEventListener, ILookOrderListener
{
    public virtual void OnLookOrder(GameEntity entity, Target target) {}
    
    protected override void Register()                 => gameEntity.AddLookOrderListener(this);
    public override    void UnregisterEventListeners() => gameEntity.RemoveLookOrderListener(this, false);
}