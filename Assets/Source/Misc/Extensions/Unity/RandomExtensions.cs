using System;
using Random = UnityEngine.Random;

public static class RandomExtensions
{
    public static Direction RandomLateralDirection()
    {
        return Random.Range(0, 4) switch
        {
            0 => Direction.Top,
            1 => Direction.Bottom,
            2 => Direction.Left,
            3 => Direction.Right,
            _ => throw new ArgumentOutOfRangeException()
        };
    }
}
