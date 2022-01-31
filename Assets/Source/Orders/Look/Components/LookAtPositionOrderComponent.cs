using UnityEngine;

[Game]
public sealed class LookAtPositionOrderComponent : IOrderComponent, IRequiresVector2Position
{
    public Vector2 position { get; set; }
}