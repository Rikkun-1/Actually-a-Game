﻿namespace UnityEngine
{
    public static class Vector2Extensions
    {
        public static Vector2Int ToVector2Int(this Vector2 value)
        {
            return Vector2Int.RoundToInt(value);
        }

        public static float ToAngle(this Vector2 value)
        {
            return Mathf.Atan2( value.x, value.y )  * Mathf.Rad2Deg;
        }
    }
}