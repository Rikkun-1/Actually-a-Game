using Entitas;
using Entitas.CodeGeneration.Attributes;

[Game]
public sealed class IDComponent : IComponent
{
    [PrimaryEntityIndex]
    public long value;
}