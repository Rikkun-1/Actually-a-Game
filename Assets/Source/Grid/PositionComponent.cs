using Entitas;
using Entitas.CodeGeneration.Attributes;
using UnityEngine;

[Game]
[Event(EventTarget.Self)]
public sealed class PositionComponent : IComponent
{
    [EntityIndex]
    public Vector2Int Value;
}