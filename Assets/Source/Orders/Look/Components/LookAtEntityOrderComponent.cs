using Entitas.CodeGeneration.Attributes;

[Game]
[Event(EventTarget.Self)]
public sealed class LookAtEntityOrderComponent : IOrderComponent
{
    public long targetID;
}