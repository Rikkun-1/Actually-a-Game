using Entitas;
using Entitas.CodeGeneration.Attributes;
using UnityEngine;

[Input]
[Unique]
public sealed class MousePositionComponent : IComponent
{
    public Vector2 position;
}