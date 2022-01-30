﻿[Game]
public sealed class ShootAtDirectionOrderComponent : IOrderComponent
{
    private readonly Angle _angle;

    public ShootAtDirectionOrderComponent()
    {
        _angle = new Angle();
    }

    public float angle
    {
        get => _angle.value;
        set => _angle.value = value;
    }
}