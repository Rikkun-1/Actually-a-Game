using UnityEngine;

public static class AngleHelper
{
    public static float RotateAngleTowards(float from, float to, float maxAngleDelta)
    {
        var current = Quaternion.Euler(0, from, 0);
        var ordered = Quaternion.Euler(0, to,   0);

        var newAngle = Quaternion.RotateTowards(current, ordered, maxAngleDelta).eulerAngles.y;

        return newAngle;
    }
}