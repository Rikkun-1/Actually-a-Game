using UnityEngine;

[Game]
public sealed class ShootAtDirectionOrderComponent : IOrderComponent, IRequiresDirection
{
    public Vector2 direction { get; set; }
    public float   angle     => direction.ToAngle();
}