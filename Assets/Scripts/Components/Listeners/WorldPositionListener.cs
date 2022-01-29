using UnityEngine;

public class WorldPositionListener : EventListener, IWorldPositionListener
{ 
    public void OnWorldPosition(GameEntity e, Vector3 newPosition)
    {
        transform.position = newPosition;
    }
        
    protected override void Register()                 => gameEntity.AddWorldPositionListener(this);
    public override    void UnregisterEventListeners() => gameEntity.RemoveWorldPositionListener(this, false);
}