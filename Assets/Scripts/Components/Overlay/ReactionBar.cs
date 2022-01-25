using Entitas;
using UnityEngine;
using UnityEngine.UI;

public class ReactionBar : MonoBehaviour, IEventListener, IReactionStartTimeListener
{
    public Image reactionBarImage;

    private float _reactionStartTime;
    private float _reactionDelay;

    private GameEntity _entity;

    private void Update()
    {
        var timePassed = GameTime.timeFromStart - _reactionStartTime;
        var percent    = timePassed / _reactionDelay;
        
        reactionBarImage.fillAmount = 1 - percent;
    }

    public void RegisterEventListeners(IEntity entity)
    {
        _entity = (GameEntity)entity;
        _entity.AddReactionStartTimeListener(this);

        if (_entity.hasReactionStartTime) OnReactionStartTime(_entity, _entity.reactionStartTime.value);
    }

    public void UnregisterEventListeners()
    {
        _entity.RemoveReactionStartTimeListener(this, false);
    }

    public void OnReactionStartTime(GameEntity entity, float reactionStartTime)
    {
        _reactionDelay     = entity.reactionDelay.value;
        _reactionStartTime = reactionStartTime;
    }
}