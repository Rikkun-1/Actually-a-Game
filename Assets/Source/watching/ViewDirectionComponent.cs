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
    private int _directionAngle;
    private int _viewingAngle;
    private int _distance;

    public int directionAngle
    {
        get => _directionAngle;
        private set => _directionAngle = value % 360;
    }

    public int viewingAngle
    {
        get => _viewingAngle;
        private set => _viewingAngle = Mathf.Clamp(value, 0, 360);
    }

    public int distance
    {
        get => _distance;
        private set => _distance = Mathf.Max(0, value);
    }

    public Vision()
    {
        directionAngle = 0;
        viewingAngle   = 0;
        distance       = 0;
    }

    public Vision(int directionAngle, int viewingAngle, int distance)
    {
        this.directionAngle = directionAngle;
        this.viewingAngle   = viewingAngle;
        this.distance       = distance;
    }
}