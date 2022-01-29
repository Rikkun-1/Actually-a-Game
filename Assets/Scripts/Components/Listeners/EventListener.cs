using UnityEngine;

public abstract class EventListener : MonoBehaviour, IEventListener
{
    protected GameEntity gameEntity;

    public virtual void RegisterEventListeners(GameEntity entity)
    {
        gameEntity = entity;
        Register();
    }

    protected  abstract void Register();
    
    public abstract void UnregisterEventListeners();
}