using Entitas.CodeGeneration.Attributes;

[Game]
[Event(EventTarget.Self)]
public sealed class LookAtEntityOrderComponent : IOrderComponent, IRequiresTargetID
{
    public long targetID { get; set; }
}