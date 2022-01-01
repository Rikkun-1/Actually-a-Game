using System.Collections.Generic;
using Entitas;
using UnityEngine;

[Game]
public sealed class PathComponent : IComponent
{
    public int              currentIndex;
    public List<Vector2Int> waypoints;
}