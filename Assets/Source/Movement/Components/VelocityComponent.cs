using Entitas;
using Entitas.CodeGeneration.Attributes;
using UnityEngine;

[Game]
[Event(EventTarget.Self)]
public sealed class VelocityComponent : IComponent
{
    public Vector3 value;
}