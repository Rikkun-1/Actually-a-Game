using System;

public static class Array2DExtensions
{
    public static T[,] Fill<T>(this T[,] array, T value)
    {
        return array.Select(_ => value);
    }

    public static T[,] DeepCopy2DArray<T>(this T[,] array)
    {
        return array.Select((x, y, value) => value);
    }

    public static T[,] Select<T>(this T[,] array, Func<T, T> func)
    {
        return array.Select((x, y, value) => func(value));
    }

    public static T[,] Select<T>(this T[,] array, Func<int, int, T, T> func)
    {
        var width  = array.GetLength(0);
        var height = array.GetLength(1);

        var result = new T[width, height];

        for (var x = 0; x < width; x++)
        {
            for (var y = 0; y < height; y++)
            {
                result[x, y] = func(x, y, array[x, y]);
            }
        }

        return result;
    }
}