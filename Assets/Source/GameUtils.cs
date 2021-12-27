using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using DesperateDevs.Utils;

public static class GameUtils
{
    public static IEnumerable<string> GetInterfaceImplementers<T>()
    {
        var types        = Assembly.GetAssembly(typeof(T)).GetTypes();
        var implementers = types.Where(type => type.IsClass && type.ImplementsInterface<T>());

        var names = implementers.Select(implementer => implementer.Name).ToList();

        names.Sort();
        return names;
    }
}