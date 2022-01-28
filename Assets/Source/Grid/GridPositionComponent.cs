using Entitas;
using Entitas.CodeGeneration.Attributes;
using UnityEngine;

[Game]
public sealed class GridPositionComponent : IComponent
{
    [EntityIndex]
    public Vector2Int value;
}