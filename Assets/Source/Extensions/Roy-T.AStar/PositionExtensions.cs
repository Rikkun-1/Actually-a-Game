using UnityEngine;

namespace Roy_T.AStar.Primitives
{
    public static class PositionExtensions
    {
        public static Vector3 ToVector3(this Position value)
        {
            return new Vector3(value.X, value.Y);
        }

        public static Vector3 ToVector3XZ(this Position value)
        {
            return new Vector3(value.X, 0, value.Y);
        }

        public static Vector2 ToVector2(this Position value)
        {
            return new Vector2(value.X, value.Y);
        }

        public static Vector2Int ToVector2Int(this Position value)
        {
            return new Vector2Int((int)value.X, (int)value.Y);
        }
    }
}