using System;
using Entitas;
using UnityEngine;

public class ReactionStartTimeListener : MonoBehaviour, IEventListener, IReactionStartTimeListener
{
    public event Action<float> OnReactionStartTimeChanged;
    
    private GameEntity _entity;

    private void Start()
    {
        if (_entity.hasReactionStartTime) OnReactionStartTime(_entity, _entity.reactionStartTime.value);
    }
    
    public void RegisterEventListeners(IEntity entity)
    {
        _entity = (GameEntity)entity;
        _entity.AddReactionStartTimeListener(this);
    }

    public void UnregisterEventListeners()
    {
        _entity.RemoveReactionStartTimeListener(this, false);
    }

    public void OnReactionStartTime(GameEntity entity, float reactionStartTime)
    {
        OnReactionStartTimeChanged?.Invoke(reactionStartTime);
    }
}