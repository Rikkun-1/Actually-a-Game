using Entitas;
using Entitas.CodeGeneration.Attributes;

using UnityEngine;

[Unique]
public class MapSizeComponent : IComponent
{
    public Vector2Int Value;
}