using Entitas;
using UnityEngine;

[Game]
public sealed class MoveToPositionOrderComponent : IComponent
{
    public Vector2Int position;
}