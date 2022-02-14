using System;

public static class FloatExtensions
{
    public static bool Equals(this float first, float second, float tolerance)
    {
        return Math.Abs(first - second) < tolerance;
    }
}