using Entitas;
using Entitas.CodeGeneration.Attributes;
using UnityEngine;

[Game] [Unique]
public sealed class MapSizeComponent : IComponent
{
    public Vector2Int value;
}