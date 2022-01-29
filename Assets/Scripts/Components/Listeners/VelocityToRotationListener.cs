using UnityEngine;

class VelocityToRotationListener : VelocityListener
{
    public override void OnVelocity(GameEntity entity, Vector3 newVelocity)
    {
        transform.rotation = Quaternion.LookRotation(newVelocity);
    }
}