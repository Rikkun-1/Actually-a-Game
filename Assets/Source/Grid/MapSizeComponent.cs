using UnityEngine;
using Entitas;
using Entitas.CodeGeneration.Attributes;

[Unique]
public class MapSizeComponent : IComponent
{
    public Vector2Int value;
}