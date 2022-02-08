using Entitas.CodeGeneration.Attributes;

[Game]
[Event(EventTarget.Self)]
public sealed class LookOrderComponent : IOrderComponent, IRequiresTarget
{
    public Target target { get; set; }
}