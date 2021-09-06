using Entitas;
using Entitas.CodeGeneration.Attributes;

[Game]
public class IDComponent : IComponent
{
    [PrimaryEntityIndex]
    public long value;
}