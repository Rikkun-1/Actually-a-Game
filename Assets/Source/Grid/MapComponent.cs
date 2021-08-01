using UnityEngine;
using Entitas;
using Entitas.CodeGeneration.Attributes;

[Unique]
public class MapComponent : IComponent
{
    public Vector2Int mapSize;
}