using UnityEngine;

public static class Vector3Extensions
{
    public static Vector3 WithY(this Vector3 value, float y)
    {
        return new Vector3(value.x, y, value.z);
    }
}