using UnityEngine;

[Game]
public sealed class MoveToPositionOrderComponent : IOrderComponent, IRequiresVector2IntPosition
{
    public Vector2Int position { get; set; }
}