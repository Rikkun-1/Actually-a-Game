using UnityEngine;

[Game]
public sealed class ShootAtPositionOrderComponent : IOrderComponent, IRequiresVector2Position
{
    public Vector2 position { get; set; }
}