using Entitas;
using ProceduralToolkit;
using UnityEngine;

public class ViewAngleListener : MonoBehaviour, IEventListener, IVisionListener
{
    private GameEntity _entity;

    public void OnDrawGizmos()
    {
        if (_entity.hasVision)
        {
            var vision         = _entity.vision;
            var directionAngle = vision.directionAngle;
            var viewingAngle   = vision.viewingAngle;
            var distance       = vision.distance;

            var direction      = transform.rotation * Vector3.forward * distance;
            var directionLeft  = 
                Quaternion.Euler(0, directionAngle - viewingAngle / 2f, 0) * Vector3.forward * distance;
            var directionRight = Quaternion.Euler(0, directionAngle + viewingAngle / 2f, 0) * Vector3.forward * distance;

            var position = transform.position;
            Debug.DrawRay(position, direction,      Color.red);
            Debug.DrawRay(position, directionLeft,  Color.red);
            Debug.DrawRay(position, directionRight, Color.red);

            DebugE.DrawWireArcXZ(position,
                                 Quaternion.identity,
                                 distance,
                                 directionAngle - viewingAngle / 2f,
                                 directionAngle + viewingAngle / 2f,
                                 Color.red);

            if (_entity.hasLookAtDirectionOrder)
            {
                direction = 
                    Quaternion.Euler(0, _entity.lookAtDirectionOrder.angle, 0) * Vector3.forward * distance;
                Debug.DrawRay(position, direction, Color.red);
            }

            if (_entity.hasLookAtPositionOrder)
            {
                var targetPosition = _entity.lookAtPositionOrder.position;
                var dir            = targetPosition - _entity.worldPosition.value;
                
                Debug.DrawRay(_entity.worldPosition.value.ToVector3XZ(), dir.ToVector3XZ(), Color.red);
            }
        }
    }

    public void RegisterEventListeners(IEntity entity)
    {
        _entity = (GameEntity)entity;
        _entity.AddVisionListener(this);

        if (_entity.hasVision)
        {
            var vision = _entity.vision;
            OnVision(_entity, vision.directionAngle, vision.viewingAngle, vision.distance, vision.turningSpeed);
        }
    }

    public void UnregisterEventListeners()
    {
        _entity.RemoveVisionListener(this, false);
    }
    
    public void OnVision(GameEntity entity, float directionAngle, int viewingAngle, int distance, int turningSpeed)
    {
        transform.rotation = Quaternion.Euler(0, directionAngle, 0);
    }
}