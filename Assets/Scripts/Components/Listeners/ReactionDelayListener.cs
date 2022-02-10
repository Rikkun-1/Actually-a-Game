using System;
using UnityEngine;
using Entitas;

public class ReactionDelayListener : MonoBehaviour, IEventListener, IReactionDelayListener
{
    public event Action<float> OnReactionDelayChanged;

    private GameEntity _entity;

    private void Start()
    {
        if (_entity.hasReactionDelay) OnReactionDelay(_entity, _entity.reactionDelay.value);
    }

    public void RegisterEventListeners(IEntity entity)
    {
        _entity = (GameEntity)entity;
        _entity.AddReactionDelayListener(this);
    }

    public void UnregisterEventListeners()
    {
        _entity.RemoveReactionDelayListener(this, false);
    }

    public void OnReactionDelay(GameEntity entity, float newReactionDelay)
    {
        OnReactionDelayChanged?.Invoke(newReactionDelay);
    }
}