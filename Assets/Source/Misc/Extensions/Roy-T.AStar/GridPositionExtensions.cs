using System;
using Roy_T.AStar.Primitives;
using UnityEngine;

public static class GridPositionExtensions
{
    public static Vector2Int ToVector2Int(this GridPosition position)
    {
        return new Vector2Int(position.X, position.Y);
    }

    public static GridPosition StepInDirection(this GridPosition from, Direction direction)
    {
        var x = from.X;
        var y = from.Y;
        
        return direction switch
        {
            Direction.Top         => new GridPosition(x,     y + 1),
            Direction.Bottom      => new GridPosition(x,     y - 1),
            Direction.Right       => new GridPosition(x + 1, y),
            Direction.Left        => new GridPosition(x - 1, y),
            Direction.TopRight    => new GridPosition(x + 1, y + 1),
            Direction.TopLeft     => new GridPosition(x - 1, y + 1),
            Direction.BottomRight => new GridPosition(x + 1, y - 1),
            Direction.BottomLeft  => new GridPosition(x - 1, y - 1),
            _                     => throw new ArgumentOutOfRangeException(nameof(direction), direction, null)
        };
    }
}