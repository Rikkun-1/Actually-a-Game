public static class VisionHelper
{
    public static void RotateEntityVisionTowards(GameEntity entityWithVision, float desiredAngle, float maxAngleDelta)
    {
        var vision = entityWithVision.vision;
        var newAngle = AngleHelper.RotateAngleTowards(entityWithVision.vision.directionAngle,
                                                      desiredAngle,
                                                      maxAngleDelta);
        entityWithVision.ReplaceVision(newAngle, vision.viewingAngle, vision.distance, vision.turningSpeed);
    }
}