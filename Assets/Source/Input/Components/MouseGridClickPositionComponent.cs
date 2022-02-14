using UnityEngine;
using Entitas;
using Entitas.CodeGeneration.Attributes;

[Input]
[Unique]
public sealed class MouseGridClickPositionComponent : IComponent
{
    public Vector2Int value;
}