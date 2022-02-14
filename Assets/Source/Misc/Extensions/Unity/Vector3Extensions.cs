using ProceduralToolkit;
using UnityEngine;

public static class Vector3Extensions
{
    public static Vector3 WithY(this Vector3 value, float y)
    {
        return new Vector3(value.x, y, value.z);
    }

    public static Vector2Int ToVector2XZInt(this Vector3 value)
    {
        return value.ToVector2XZ().ToVector2Int();
    }
    
    public static Vector3 Randomize(this Vector3 value, float min, float max)
    {
        value.x = GameRandom.Range(-min, max);
        value.y = GameRandom.Range(-min, max);
        value.z = GameRandom.Range(-min, max);
        
        return value;
    }
}