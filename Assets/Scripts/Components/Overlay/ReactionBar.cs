using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(ReactionDelayListener))]
[RequireComponent(typeof(ReactionStartTimeListener))]
[RequireComponent(typeof(ReactionStartTimeRemovedListener))]
public class ReactionBar : MonoBehaviour
{
    public Image reloadingBarImage;

    private float _reactionStartTime;
    private float _reactionDelay;

    private ReactionDelayListener            _cachedReactionDelayListener;
    private ReactionStartTimeListener        _cachedReactionStartTimeListener;
    private ReactionStartTimeRemovedListener _cachedReactionStartTimeRemovedListener;
    
    private void Start()
    {
        _cachedReactionDelayListener            = GetComponent<ReactionDelayListener>();
        _cachedReactionStartTimeListener        = GetComponent<ReactionStartTimeListener>();
        _cachedReactionStartTimeRemovedListener = GetComponent<ReactionStartTimeRemovedListener>();
        
        _cachedReactionDelayListener.OnReactionDelayChanged                     += UpdateReactionDelay;
        _cachedReactionStartTimeListener.OnReactionStartTimeChanged             += UpdateReactionStartTime;
        _cachedReactionStartTimeRemovedListener.OnAfterReactionStartTimeRemoved += Hide;
    }

    private void OnDestroy()
    {
        _cachedReactionDelayListener.OnReactionDelayChanged                     -= UpdateReactionDelay;
        _cachedReactionStartTimeListener.OnReactionStartTimeChanged             -= UpdateReactionStartTime;
        _cachedReactionStartTimeRemovedListener.OnAfterReactionStartTimeRemoved -= Hide;
    }

    private void Hide()
    {
        reloadingBarImage.enabled = false;
    }
    
    private void UpdateReactionDelay(float newReactionDelay)
    {
        _reactionDelay = newReactionDelay;
    }

    private void UpdateReactionStartTime(float newReactionStartTime)
    {
        reloadingBarImage.enabled = true;
        _reactionStartTime        = newReactionStartTime;
    }

    private void Update()
    {
        var timePassed = GameTime.timeFromStart - _reactionStartTime;
        var percent    = timePassed / _reactionDelay;

        reloadingBarImage.fillAmount = 1 - percent;
    }
}