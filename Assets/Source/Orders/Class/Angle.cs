﻿using System;
using UnityEngine;

public class Angle
{
    private float _value;

    public float value
    {
        get => _value;
        set
        {
            var val = Mathf.Repeat(value, 360);

            _value = (float)Math.Round(val, 2);
        }
    }
    
    public Angle()
    {
    }

    public Angle(float value)
    {
        this.value = value;
    }
    
    public static implicit operator float(Angle angle)
    {
        return angle.value;
    }
}