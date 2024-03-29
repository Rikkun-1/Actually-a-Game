﻿using ProceduralToolkit;
using UnityEngine;

public static class AimHelper
{
    public static bool IsAimingAtTargetDirection(GameEntity e, float targetDirection)
    {
        return e.vision.directionAngle.Equals(targetDirection, 0.01f);
    }

    public static bool IsAimingAtTargetPosition(GameEntity e, Vector2 targetPosition)
    {
        var shooterPosition = e.worldPosition.value.ToVector2XZ();
        var targetDirection = targetPosition - shooterPosition;
        var angleToTarget   = targetDirection.ToAngle360();

        return IsAimingAtTargetDirection(e, angleToTarget);
    }

    public static bool IsAimingAtTargetEntity(GameEntity e, GameEntity targetEntity)
    {
        var targetPosition = targetEntity.worldPosition.value.ToVector2XZ();

        return IsAimingAtTargetPosition(e, targetPosition);
    }
}