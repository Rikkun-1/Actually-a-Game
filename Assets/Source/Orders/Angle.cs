using System;
using UnityEngine;

public class Angle
{
    private float _value;

    public Angle()
    {
    }

    public Angle(float value)
    {
        this.value = value;
    }

    public float value
    {
        get => _value;
        set
        {
            var val = Mathf.Repeat(value, 360);
            
            _value = (float)Math.Round(val, 2);
        }
    }
}