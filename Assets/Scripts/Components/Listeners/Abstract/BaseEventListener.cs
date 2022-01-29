using UnityEngine;

[AddComponentMenu("")] // hide in component menu
public abstract class BaseEventListener : MonoBehaviour, IEventListener
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