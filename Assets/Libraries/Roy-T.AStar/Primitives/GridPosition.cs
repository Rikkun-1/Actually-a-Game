using System;
using UnityEngine;

namespace Roy_T.AStar.Primitives
{
    public struct GridPosition : IEquatable<GridPosition>
    {
        public static GridPosition Zero => new GridPosition(0, 0);

        public GridPosition(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }

        public int X { get; }
        public int Y { get; }

        public static bool operator ==(GridPosition a, GridPosition b)
            => a.Equals(b);

        public static bool operator !=(GridPosition a, GridPosition b)
            => !a.Equals(b);

        public override string ToString() => $"({this.X}, {this.Y})";

        public override bool Equals(object obj) => obj is GridPosition GridPosition && this.Equals(GridPosition);

        public bool Equals(GridPosition other) => this.X == other.X && this.Y == other.Y;

        public override int GetHashCode() => -1609761766 + this.X + this.Y;
    }
    
    public static class GridPositionExtension
    {
        public static Vector2Int ToVector2Int(this GridPosition position)
        {
            return new Vector2Int(position.X, position.Y);
        }
    }
}