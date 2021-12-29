using ProceduralToolkit;
using UnityEngine;

public static class RaycastHelper
{
    public static bool IsInClearVision(GameEntity e, GameEntity targetEntity)
    {
        if(!targetEntity.hasUnityView) return false;
        
        var raycastOrigin = e.worldPosition.value.ToVector3XZ();
        raycastOrigin.y = 0.25f;

        var targetPosition = targetEntity.worldPosition.value.ToVector3XZ();
        targetPosition.y = 0.25f;

        var raycastDirection = targetPosition - raycastOrigin;
        var maxDistance      = Vector3.Distance(raycastOrigin, targetPosition);
        var raycastHits      = Physics.RaycastAll(raycastOrigin, raycastDirection, maxDistance);
        
        return raycastHits.Length == 1;
    }
}