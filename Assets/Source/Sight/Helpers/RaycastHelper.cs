using ProceduralToolkit;
using UnityEngine;

public static class RaycastHelper
{
    public static bool IsInClearVision(GameEntity e, GameEntity targetEntity)
    {
        if(!targetEntity.hasUnityView) return false;
        
        var raycastOrigin  = e.worldPosition.value.ToVector3XZ().WithY(0.8f);
        var targetPosition = targetEntity.worldPosition.value.ToVector3XZ().WithY(0.8f);

        var raycastDirection = targetPosition - raycastOrigin;
        var maxDistance      = Vector3.Distance(raycastOrigin, targetPosition);
        var raycastHits      = Physics.RaycastAll(raycastOrigin, raycastDirection, maxDistance);
        
        return raycastHits.Length == 1;
    }
}