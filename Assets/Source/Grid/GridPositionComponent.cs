using UnityEngine;
using Entitas;
using Entitas.CodeGeneration.Attributes;

[Game]
public sealed class GridPositionComponent : IComponent
{
    [EntityIndex]
    public Vector2Int value;
}