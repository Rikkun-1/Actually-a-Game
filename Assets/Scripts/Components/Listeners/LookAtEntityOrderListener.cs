using UnityEngine;

[RequireComponent(typeof(Aiming))]
public class LookAtEntityOrderListener : EventListener, ILookAtEntityOrderListener
{
    private Aiming _aiming;

    private void Start()
    {
        _aiming = GetComponent<Aiming>();
    }

    public void OnLookAtEntityOrder(GameEntity entity, long targetID)
    {
        var targetEntity     = Contexts.sharedInstance.game.GetEntityWithId(targetID);
        var targetGameObject = targetEntity.unityView.gameObject;
        var targetTransform  = targetGameObject.GetComponent<AimingPoints>().torso;
        _aiming.target = targetTransform;
    }
    
    protected override void Register()                 => gameEntity.AddLookAtEntityOrderListener(this);
    public override    void UnregisterEventListeners() => gameEntity.RemoveLookAtEntityOrderListener(this, false);
}