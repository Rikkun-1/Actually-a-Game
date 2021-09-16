using Entitas;
using Entitas.CodeGeneration.Attributes;
using UnityEngine;

[Game]
[Event(EventTarget.Self)]
public sealed class VisionComponent : IComponent
{
    private readonly Angle _directionAngle;
    private          int   _distance;
    private          int   _turningSpeed;
    private          int   _viewingAngle;

    public VisionComponent()
    {
        _directionAngle = new Angle();
    }
    
    public VisionComponent(float directionAngle, int viewingAngle, int distance, int turningSpeed) : this()
    {
        this.directionAngle = directionAngle;
        this.viewingAngle   = viewingAngle;
        this.distance       = distance;
        this.turningSpeed   = turningSpeed;
    }

    public float directionAngle
    {
        get => _directionAngle.value;
        set => _directionAngle.value = value;
    }

    public int viewingAngle
    {
        get => _viewingAngle;
        set => _viewingAngle = Mathf.Clamp(value, 0, 360);
    }

    public int distance
    {
        get => _distance;
        set => _distance = Mathf.Max(0, value);
    }

    public int turningSpeed
    {
        get => _turningSpeed;
        set => _turningSpeed = Mathf.Max(0, value);
    }
}