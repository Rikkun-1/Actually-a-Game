public static class VisionHelper
{
    public static void RotateEntityVisionTowards(GameEntity entityWithVision, float desiredAngle, float maxAngleDelta)
    {
        var newAngle = AngleHelper.RotateAngleTowards(entityWithVision.vision.directionAngle, desiredAngle, maxAngleDelta);
        entityWithVision.vision.directionAngle = newAngle;
        entityWithVision.UpdateVision();
    }
}