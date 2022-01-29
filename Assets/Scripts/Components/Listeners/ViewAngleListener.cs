using ProceduralToolkit;
using UnityEngine;

public class ViewAngleListener : EventListener, IVisionListener
{
    public void OnDrawGizmos()
    {
        if (gameEntity.hasVision)
        {
            var vision         = gameEntity.vision;
            var directionAngle = vision.directionAngle;
            var viewingAngle   = vision.viewingAngle;
            var distance       = vision.distance;

            var direction = transform.rotation * Vector3.forward * distance;
            var directionLeft =
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

            // if (_entity.hasLookAtDirectionOrder)
            // {
            //     direction =
            //         Quaternion.Euler(0, _entity.lookAtDirectionOrder.angle, 0) * Vector3.forward * distance;
            //     Debug.DrawRay(position, direction, Color.red);
            // }
            //
            // if (_entity.hasLookAtPositionOrder)
            // {
            //     var targetPosition = _entity.lookAtPositionOrder.position;
            //     var dir            = targetPosition - _entity.worldPosition.value;
            //
            //     Debug.DrawRay(_entity.worldPosition.value.ToVector3XZ(), dir.ToVector3XZ(), Color.red);
            // }
        }
    }
    
    public void OnVision(GameEntity entity, float directionAngle, int viewingAngle, int distance, int turningSpeed)
    {
        transform.rotation = Quaternion.Euler(0, directionAngle, 0);
    }
    
    protected override void Register()                 => gameEntity.AddVisionListener(this);
    public override    void UnregisterEventListeners() => gameEntity.RemoveVisionListener(this, false);
}