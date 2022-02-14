using Roy_T.AStar.Primitives;
using UnityEngine;

public static class Vector2IntExtensions
{
    public static GridPosition ToGridPosition(this Vector2Int position)
    {
        return new GridPosition(position.x, position.y);
    }

    public static Vector3 ToVector3(this Vector2Int value)
    {
        return new Vector3(value.x, value.y);
    }

    public static Vector3 ToVector3XZ(this Vector2Int value)
    {
        return new Vector3(value.x, 0, value.y);
    }

    public static float ToAngle(this Vector2Int value)
    {
        var vector = (Vector2)value;
        return vector.ToAngle360();
    }
    
    public static Vector2Int Average(Vector2Int[] values)
    {
        var average = Vector2Int.zero;
        foreach (var value in values)
        {
            average += value;
        }
        
        average /= values.Length;
        
        return average;
    }
}