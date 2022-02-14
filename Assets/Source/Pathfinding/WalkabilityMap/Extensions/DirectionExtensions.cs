using System;

public static class DirectionExtension
{
    public static Direction OppositeDirection(this Direction direction)
    {
        return direction switch
        {
            Direction.Top         => Direction.Bottom,
            Direction.Bottom      => Direction.Top,
            Direction.Right       => Direction.Left,
            Direction.Left        => Direction.Right,
            Direction.TopRight    => Direction.BottomLeft,
            Direction.BottomLeft  => Direction.TopRight,
            Direction.BottomRight => Direction.TopLeft,
            Direction.TopLeft     => Direction.BottomRight,
            _                     => throw new ArgumentOutOfRangeException(nameof(direction), direction, null)
        };
    }

    public static (Direction left, Direction right) GetNeighbours(this Direction direction)
    {
        return direction switch
        {
            Direction.TopRight    => (Direction.Top, Direction.Right),
            Direction.BottomLeft  => (Direction.Bottom, Direction.Left),
            Direction.BottomRight => (Direction.Right, Direction.Bottom),
            Direction.TopLeft     => (Direction.Left, Direction.Top),
            Direction.Top         => (Direction.TopLeft, Direction.TopRight),
            Direction.Bottom      => (Direction.BottomRight, Direction.BottomLeft),
            Direction.Right       => (Direction.TopRight, Direction.BottomRight),
            Direction.Left        => (Direction.BottomLeft, Direction.TopLeft),
            _                     => throw new ArgumentOutOfRangeException(nameof(direction), direction, null)
        };
    }
    
    public static bool IsLateralDirection(this Direction direction)
    {
        switch (direction)
        {
            case Direction.Top:
            case Direction.Right:
            case Direction.Bottom:
            case Direction.Left: return true;
            default: return false;
        }
    }
    
    public static bool IsDiagonalDirection(this Direction direction)
    {
        switch (direction)
        {
            case Direction.TopRight:
            case Direction.TopLeft:
            case Direction.BottomRight:
            case Direction.BottomLeft: return true;
            default: return false;
        }
    }
}