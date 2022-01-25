using Entitas;
using UnityEngine;

[RequireComponent(typeof(Aiming))]
public class LookAtEntityOrderListener : MonoBehaviour, IEventListener, ILookAtEntityOrderListener
{
    private Aiming     _aiming;
    private GameEntity _entity;

    public void RegisterEventListeners(IEntity entity)
    {
        _aiming = GetComponent<Aiming>();
        
        _entity = (GameEntity)entity;
        _entity.AddLookAtEntityOrderListener(this);

        if (_entity.hasLookAtEntityOrder) OnLookAtEntityOrder(_entity, _entity.lookAtEntityOrder.targetID);
    }

    public void UnregisterEventListeners()
    {
        _entity.RemoveLookAtEntityOrderListener(this, false);
    }

    public void OnLookAtEntityOrder(GameEntity entity, long targetID)
    {
        var targetEntity     = Contexts.sharedInstance.game.GetEntityWithId(targetID);
        var targetGameObject = targetEntity.unityView.gameObject;
        var targetTransform  = targetGameObject.GetComponent<AimingPoints>().torso;
        _aiming.target = targetTransform;
    }
}