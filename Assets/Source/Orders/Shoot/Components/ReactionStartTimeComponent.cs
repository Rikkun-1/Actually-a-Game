using Entitas;
using Entitas.CodeGeneration.Attributes;

[Game]
[Event(EventTarget.Self)]
[Event(EventTarget.Self, EventType.Removed)]
public sealed class ReactionStartTimeComponent : IComponent
{
    public float value;
}