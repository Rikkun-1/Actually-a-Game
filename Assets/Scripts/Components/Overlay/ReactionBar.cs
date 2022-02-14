using UnityEngine.UI;

public class ReactionBar : BaseReactionListener
{
    public Image reloadingBarImage;

    private float _reactionStartTime;
    private float _reactionDelay;

    public override void OnReactionDelay(GameEntity            entity, float newReactionDelay)  => UpdateReactionDelay(newReactionDelay);
    public override void OnReactionStartTime(GameEntity        entity, float reactionStartTime) => UpdateReactionStart(reactionStartTime);
    public override void OnReactionStartTimeRemoved(GameEntity entity) => HideBar();

    private void UpdateReactionDelay(float newReactionDelay) => _reactionDelay = newReactionDelay;
    
    private void UpdateReactionStart(float reactionStartTime)
    {
        reloadingBarImage.enabled = true;
        _reactionStartTime        = reactionStartTime;
    }
    
    private void HideBar() => reloadingBarImage.enabled = false;

    private void Update()
    {
        var timePassed = GameTime.timeFromStart - _reactionStartTime;
        var percent    = timePassed / _reactionDelay;

        reloadingBarImage.fillAmount = 1 - percent;
    }
}