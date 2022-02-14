using UnityEngine;

[AddComponentMenu("")] // hide in component menu
public abstract class BaseWorldPositionListener : BaseEventListener, IWorldPositionListener
{
    public virtual void OnWorldPosition(GameEntity e, Vector3 newPosition) {}
        
    protected override void Register()                 => gameEntity.AddWorldPositionListener(this);
    public override    void UnregisterEventListeners() => gameEntity.RemoveWorldPositionListener(this, false);
}