using ProceduralToolkit;
using UnityEngine;

public class ViewAngleListener : BaseVisionListener
{
    public void OnDrawGizmos()
    {
        if (!gameEntity.hasVision) return;
        
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
    }
    
    public override void OnVision(GameEntity entity, float directionAngle, int viewingAngle, int distance, int turningSpeed)
    {
        transform.rotation = Quaternion.Euler(0, directionAngle, 0);
    }
}