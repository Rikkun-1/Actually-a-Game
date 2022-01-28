using System.Collections.Generic;
using Entitas;

[Game]
public sealed class OrderSequence : IComponent
{
    public List<Dictionary<string, string>> orders;
}