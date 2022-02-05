﻿using UnityEngine;

public static class RaycastHelper
{
    private static readonly LayerMask _layerMask;

    static RaycastHelper()
    {
        _layerMask.value = LayerMask.GetMask("Default", "Vision Check");
    }

    public static bool IsInClearVision(Vector3 origin, Vector3 target)
    {
        var raycastDirection = target - origin;
        return Physics.Raycast(origin, raycastDirection, Vector3.Distance(origin, target)) == false;
    }

    public static bool IsInClearVision(Vector3 origin, GameEntity targetEntity)
    {
        if (!targetEntity.hasUnityView) return false;

        var targetPosition   = targetEntity.worldPosition.value.WithY(1.4f);
        var raycastDirection = targetPosition - origin;

        if (!Physics.Raycast(origin, raycastDirection, out var hitInfo, Vector3.Distance(origin, targetPosition), _layerMask)) return false;

        var entity = hitInfo.collider.GetGameEntity();
        return entity.id.value == targetEntity.id.value;
    }

    public static bool IsInClearVision(GameEntity e, GameEntity targetEntity)
    {
        var raycastOrigin = e.worldPosition.value.WithY(1.4f);

        return IsInClearVision(raycastOrigin, targetEntity);
    }
    
    public static bool Linecast(Vector3 start, Vector3 end, out RaycastHit raycastHit, int layerMask)
    {
        var direction = end - start;
        var distance  = Vector3.Distance(end, start);
        return Physics.Raycast(start, direction, out raycastHit, distance, layerMask);
    }
}