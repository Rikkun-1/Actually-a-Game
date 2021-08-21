using Entitas;
using Roy_T.AStar.Primitives;
using UnityEngine;

[Game]
public class PathRequestComponent : IComponent
{
    public Vector2Int From;
    public Vector2Int To;
}