using UnityEngine;
using Entitas;
using Entitas.CodeGeneration.Attributes;

[Game]
[Event(EventTarget.Self)]
public class WorldPositionComponent : IComponent
{
    public Vector2 value;
}