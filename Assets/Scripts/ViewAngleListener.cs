using Entitas;
using UnityEngine;
using ProceduralToolkit;

public class ViewAngleListener : MonoBehaviour, IEventListener, IVisionListener
{
    private GameEntity _entity;

    public void OnDrawGizmos()
    {
        if (_entity.hasVision)
        {
            var vision         = _entity.vision.value;
            var directionAngle = vision.directionAngle;
            var viewingAngle   = vision.viewingAngle;
            var distance       = vision.distance;

            var direction      = transform.rotation * Vector3.forward * distance;
            var directionLeft  = Quaternion.Euler(0, directionAngle - viewingAngle / 2, 0) * Vector3.forward * distance;
            var directionRight = Quaternion.Euler(0, directionAngle + viewingAngle / 2, 0) * Vector3.forward * distance;

            Debug.DrawRay(transform.position, direction,      Color.red);
            Debug.DrawRay(transform.position, directionLeft,  Color.red);
            Debug.DrawRay(transform.position, directionRight, Color.red);
        
            DebugE.DrawWireArcXZ(transform.position, Quaternion.identity, distance, directionAngle - viewingAngle / 2, 
                                 directionAngle + viewingAngle / 2, Color.red);
        
            //Handles.DrawWireArc(center, normal, from, angle, radius);
            //GizmosE.DrawWireArc(Draw.pointOnCircleXY, transform.position, transform.rotation, 2, -25, 25);
        }
    }

    public void RegisterEventListeners(IEntity entity)
    {
        _entity = (GameEntity)entity;
        _entity.AddVisionListener(this);

        if (_entity.hasVision)
        {
            var vision = _entity.vision.value;
            OnVision(_entity, vision);
            //OnVision(_entity, vision.directionAngle, vision.viewingAngle, vision.distance);  
        }
    }

    public void UnregisterEventListeners()
    {
        _entity.RemoveVisionListener(this, false);
    }

    // public void OnVision(GameEntity entity, int directionAngle, int viewingAngle, int distance)
    // {
    //     transform.rotation = Quaternion.Euler(0, 0, -directionAngle);
    // }

    public void OnVision(GameEntity entity, Vision value)
    {
        transform.rotation = Quaternion.Euler(0, value.directionAngle, 0);
    }
}