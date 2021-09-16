using Entitas;
using Entitas.CodeGeneration.Attributes;

[Game]
[Event(EventTarget.Self)]
public sealed class VisionComponent : IComponent
{
    public Vision value;
}