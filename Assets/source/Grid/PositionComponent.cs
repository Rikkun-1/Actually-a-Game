using UnityEngine;
using Entitas;
using Entitas.CodeGeneration.Attributes;

[Game, Event(EventTarget.Self)]
public sealed class PositionComponent : IComponent
{
    [EntityIndex]
    public Vector2Int value;
}
