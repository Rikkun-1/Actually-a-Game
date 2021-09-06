using System;
using Entitas;
using Entitas.CodeGeneration.Attributes;
using UnityEngine;

[Game]
[Event(EventTarget.Self)]
public class VisionComponent : IComponent
{
    public Vision value;
}

public class Vision
{
    private float _directionAngle;
    private int   _viewingAngle;
    private int   _distance;
    private int   _turningSpeed;

    public float directionAngle
    {
        get => _directionAngle;
        set => _directionAngle = (float)Math.Round(value, 2) % 360;
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
    
    public Vision()
    {
        directionAngle = 0;
        viewingAngle   = 0;
        distance       = 0;
        turningSpeed   = 0;
    }

    public Vision(int directionAngle, int viewingAngle, int distance, int turningSpeed)
    {
        this.directionAngle = directionAngle;
        this.viewingAngle   = viewingAngle;
        this.distance       = distance;
        this.turningSpeed   = turningSpeed;
    }
}