using UnityEngine;

class VelocityToRotationListener : BaseVelocityListener
{
    public override void OnVelocity(GameEntity entity, Vector3 newVelocity)
    {
        transform.rotation = Quaternion.LookRotation(newVelocity);
    }
}