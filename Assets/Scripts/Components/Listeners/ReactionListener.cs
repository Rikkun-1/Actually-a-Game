public abstract class ReactionListener: EventListener, 
                                        IReactionDelayListener, 
                                        IReactionStartTimeListener, 
                                        IReactionStartTimeRemovedListener
{
    public virtual void OnReactionDelay(GameEntity            entity, float newReactionDelay) {}
    public virtual void OnReactionStartTime(GameEntity        entity, float reactionStartTime) {}
    public virtual void OnReactionStartTimeRemoved(GameEntity entity) {}
    
    protected override void Register()
    {
        gameEntity.AddReactionDelayListener(this);
        gameEntity.AddReactionStartTimeListener(this);
        gameEntity.AddReactionStartTimeRemovedListener(this);
    }

    public override void UnregisterEventListeners()
    {
        gameEntity.RemoveReactionDelayListener(this, false);
        gameEntity.RemoveReactionStartTimeListener(this, false);
        gameEntity.RemoveReactionStartTimeRemovedListener(this, false);
    }
}
