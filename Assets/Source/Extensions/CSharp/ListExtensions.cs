using System.Collections.Generic;

public static class ListExtensions
{
    public static void AddIfNotPresented<T>(this List<T> list, T value)
    {
        if (!list.Contains(value))
        {
            list.Add(value);
        }
    }
}