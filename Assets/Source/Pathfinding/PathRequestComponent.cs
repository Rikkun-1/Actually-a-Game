using Entitas;
using UnityEngine;

[Game]
public class PathRequestComponent : IComponent
{
    public Vector2Int from;
    public Vector2Int to;
}