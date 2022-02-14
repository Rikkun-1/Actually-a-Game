using UnityEngine;

[RequireComponent(typeof(Aiming))]
public class AimToTarget : BaseLookAtEntityOrderListener
{
    private Aiming _aiming;

    private void Start()
    {
        _aiming = GetComponent<Aiming>();
    }

    public override void OnLookOrder(GameEntity entity, Target target)
    {
        if (target.targetType != TargetType.Entity) return;
        
        var targetEntity     = Contexts.sharedInstance.game.GetEntityWithId(target.entityID);
        var targetGameObject = targetEntity.unityView.gameObject;
        var targetTransform  = targetGameObject.GetComponent<AimingPoints>().torso;
        _aiming.target = targetTransform;
    }
}