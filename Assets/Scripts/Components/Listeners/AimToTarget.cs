using UnityEngine;

[RequireComponent(typeof(Aiming))]
public class AimToTarget : BaseLookAtEntityOrderListener
{
    private Aiming _aiming;

    private void Start()
    {
        _aiming = GetComponent<Aiming>();
    }

    public override void OnLookAtEntityOrder(GameEntity entity, long targetID)
    {
        var targetEntity     = Contexts.sharedInstance.game.GetEntityWithId(targetID);
        var targetGameObject = targetEntity.unityView.gameObject;
        var targetTransform  = targetGameObject.GetComponent<AimingPoints>().torso;
        _aiming.target = targetTransform;
    }
}