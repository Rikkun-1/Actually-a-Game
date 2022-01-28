using System;
using UnityEngine;

public static class AimHelper
{
    public static bool IsAimingAtTargetDirection(GameEntity e, float targetDirection)
    {
        return Math.Abs(e.vision.value.directionAngle - targetDirection) < 0.01;
    }
    
    public static bool IsAimingAtTargetPosition(GameEntity e, Vector2 targetPosition)
    {
        var shooterPosition = e.worldPosition.value;
        var targetDirection = targetPosition - shooterPosition;
        var angleToTarget   = new Angle(targetDirection.ToAngle()).value;
        
        return IsAimingAtTargetDirection(e, angleToTarget);
    }
    
    public static bool IsAimingAtTargetEntity(GameEntity e, GameEntity targetEntity)
    {
        var targetPosition  = targetEntity.worldPosition.value;

        return IsAimingAtTargetPosition(e, targetPosition);
    }
}
