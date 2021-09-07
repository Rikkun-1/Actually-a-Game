using UnityEngine;

public class Vision
{
    private Angle _directionAngle;
    private int   _distance;
    private int   _turningSpeed;
    private int   _viewingAngle;

    public Vision()
    {
    }

    public Vision(float directionAngle, int viewingAngle, int distance, int turningSpeed)
    {
        this.directionAngle = directionAngle;
        this.viewingAngle   = viewingAngle;
        this.distance       = distance;
        this.turningSpeed   = turningSpeed;
    }

    public float directionAngle
    {
        get => _directionAngle.value;
        set
        {
            if (_directionAngle == null)
            {
                _directionAngle = new Angle(value);
                return;
            }

            _directionAngle.value = value;
        }
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