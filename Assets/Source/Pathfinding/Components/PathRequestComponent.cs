using Entitas;
using UnityEngine;

[Game]
public sealed class PathRequestComponent : IComponent
{
    public Vector2Int from;
    public Vector2Int to;
}