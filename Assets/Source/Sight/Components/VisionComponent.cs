using Entitas;
using Entitas.CodeGeneration.Attributes;
using UnityEngine;

[Game]
[Event(EventTarget.Self)]
public sealed class VisionComponent : IComponent
{
    public float directionAngle;
    public int   viewingAngle;
    public int   distance;
    public int   turningSpeed;
}