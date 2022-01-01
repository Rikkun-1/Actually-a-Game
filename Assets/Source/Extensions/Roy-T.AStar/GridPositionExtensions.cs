using UnityEngine;

namespace Roy_T.AStar.Primitives
{
    public static class GridPositionExtensions
    {
        public static Vector2Int ToVector2Int(this GridPosition position)
        {
            return new Vector2Int(position.X, position.Y);
        }
    }
}