using UnityEngine;

public class WorldPositionListener : BaseWorldPositionListener
{ 
    public override void OnWorldPosition(GameEntity e, Vector3 newPosition)
    {
        transform.position = newPosition;
    }
}