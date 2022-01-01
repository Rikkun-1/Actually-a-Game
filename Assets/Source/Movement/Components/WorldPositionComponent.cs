using Entitas;
using Entitas.CodeGeneration.Attributes;
using UnityEngine;

[Game]
[Event(EventTarget.Self)]
public sealed class WorldPositionComponent : IComponent
{
    public Vector2 value;
}