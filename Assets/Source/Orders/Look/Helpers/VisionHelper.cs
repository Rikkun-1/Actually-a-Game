using UnityEngine;

public static class VisionHelper
{
    public static void RotateEntityVisionTowards(GameEntity entityWithVision, float desiredAngle, float maxAngleDelta)
    {
        var vision   = entityWithVision.vision;
        var newAngle = Mathf.Repeat(Mathf.MoveTowardsAngle(vision.directionAngle, desiredAngle, maxAngleDelta), 360f);
        vision.directionAngle = newAngle;
        entityWithVision.UpdateVision();
    }
}