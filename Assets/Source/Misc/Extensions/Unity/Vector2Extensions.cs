using UnityEngine;

public static class Vector2Extensions
{
    public static Vector2Int ToVector2Int(this Vector2 value)
    {
        return Vector2Int.RoundToInt(value);
    }

    public static Vector2 WithYDecreasedBy(this Vector2 value, float decrement)
    {
        return new Vector2(value.x, value.y - decrement);
    }

    public static float ToAngle360(this Vector2 value)
    {
        var angle = Mathf.Atan2(value.x, value.y)  * Mathf.Rad2Deg;
        return  Mathf.Repeat(angle, 360);
    }

    public static float Angle360(Vector2 from, Vector2 to)
    {
        return (to - from).ToAngle360();
    }
    
    public static Vector3 ToVector3XZInt(this Vector2 value)
    {
        return value.ToVector2Int().ToVector3XZ();
    }
}