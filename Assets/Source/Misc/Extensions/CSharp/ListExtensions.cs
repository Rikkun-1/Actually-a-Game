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
    
    public static List<T> Slice<T>(this List<T> source, int from, int to)
    {
        return from <= to 
                   ? source.GetRange(from, to - from)
                   : new List<T>();
    }
    
    public static T GetFromEnd<T>(this List<T> source, int index)
    {
        return source[source.Count - index];
    }
}