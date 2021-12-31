using ProceduralToolkit;
using UnityEngine;

public static class RaycastHelper
{
    public static bool IsInClearVision(Vector3 origin, Vector3 target)  
    {
        var raycastDirection = target - origin;
        var raycastHits      = Physics.RaycastAll(origin, raycastDirection, Vector3.Distance(origin, target));
        return raycastHits.Length == 0;
    }
    
    public static bool IsInClearVision(Vector3 origin, GameEntity targetEntity)
    {
        if (!targetEntity.hasUnityView) return false;

        var targetPosition = targetEntity.worldPosition.value.ToVector3XZ().WithY(0.4f);

        var raycastDirection = targetPosition - origin;
        var raycastHits      = Physics.RaycastAll(origin, raycastDirection, Vector3.Distance(origin, targetPosition));
        return raycastHits.Length == 1;
    }

    public static bool IsInClearVision(GameEntity e, GameEntity targetEntity)
    {
        var raycastOrigin  = e.worldPosition.value.ToVector3XZ().WithY(0.4f);

        return IsInClearVision(raycastOrigin, targetEntity);
    }
}