using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(ReactionStartTimeListener))]
[RequireComponent(typeof(ReactionDelayListener))]
public class ReactionBar : MonoBehaviour
{
    public Image reloadingBarImage;

    private float _reactionStartTime;
    private float _reactionDelay;

    private void Start()
    {
        GetComponent<ReactionStartTimeListener>().OnReactionStartTimeChanged += UpdateReactionStartTime;
        GetComponent<ReactionDelayListener>().OnReactionDelayChanged         += UpdateReactionDelay;
    }

    private void OnDestroy()
    {
        GetComponent<ReactionStartTimeListener>().OnReactionStartTimeChanged -= UpdateReactionStartTime;
        GetComponent<ReactionDelayListener>().OnReactionDelayChanged         -= UpdateReactionDelay;
    }

    private void UpdateReactionDelay(float newReactionDelay)
    {
        _reactionDelay = newReactionDelay;
    }

    private void UpdateReactionStartTime(float newReactionStartTime)
    {
        _reactionStartTime = newReactionStartTime;
    }

    private void Update()
    {
        var timePassed = GameTime.timeFromStart - _reactionStartTime;
        var percent    = timePassed / _reactionDelay;

        reloadingBarImage.fillAmount = 1 - percent;
    }
}