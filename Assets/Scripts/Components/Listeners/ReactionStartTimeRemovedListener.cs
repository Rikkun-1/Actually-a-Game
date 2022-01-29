using System;
using Entitas;
using UnityEngine;

public class ReactionStartTimeRemovedListener: MonoBehaviour, IEventListener, IReactionStartTimeRemovedListener
{
    public event Action OnAfterReactionStartTimeRemoved;

    private GameEntity _entity;
    
    public void RegisterEventListeners(IEntity entity)
    {
        _entity = (GameEntity)entity;
        _entity.AddReactionStartTimeRemovedListener(this);
    }

    public void UnregisterEventListeners()
    {
        _entity.RemoveReactionStartTimeRemovedListener(this, false);
    }

    public void OnReactionStartTimeRemoved(GameEntity entity)
    {
        OnAfterReactionStartTimeRemoved?.Invoke();
    }
}
