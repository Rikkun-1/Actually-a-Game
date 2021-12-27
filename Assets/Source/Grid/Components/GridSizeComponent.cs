using Entitas;
using Entitas.CodeGeneration.Attributes;
using UnityEngine;

[Game] [Unique]
public sealed class GridSizeComponent : IComponent
{
    public Vector2Int value;
}