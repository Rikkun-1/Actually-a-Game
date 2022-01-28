using System.Collections.Generic;
using Entitas;

[Game]
public class OrderSequence : IComponent
{
    public List<Dictionary<string, string>> orders;
}