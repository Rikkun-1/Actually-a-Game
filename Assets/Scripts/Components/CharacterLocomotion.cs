using UnityEngine;

[RequireComponent(typeof(Animator))]
public class CharacterLocomotion : MonoBehaviour
{
    private static readonly int   _velXHash = Animator.StringToHash("Vel X");
    private static readonly int   _velZHash = Animator.StringToHash("Vel Z");
    public                  float DefaultAnimationWalkSpeed;

    public float transitionSpeed;

    private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public void CalculateAnimationVelocity(Vector3 movement)
    {
        _animator.speed = Mathf.Clamp(movement.magnitude / DefaultAnimationWalkSpeed, 1, float.MaxValue);

        var globalMove = Vector3.forward * movement.z + Vector3.right * movement.x;
        var localMove  = transform.forward * movement.z + transform.right * movement.x;

        var angle = Vector3.SignedAngle(localMove, globalMove, Vector3.up);

        var newAnimationDirection    = Quaternion.Euler(0, angle, 0) * globalMove / DefaultAnimationWalkSpeed;
        var smoothAnimationDirection = GetSmoothAnimationDirection(newAnimationDirection);

        _animator.SetFloat(_velXHash, smoothAnimationDirection.x);
        _animator.SetFloat(_velZHash, smoothAnimationDirection.z);
    }

    private Vector3 GetSmoothAnimationDirection(Vector3 newAnimationDirection)
    {
        var currentAnimationDirection = new Vector3
        {
            x = _animator.GetFloat(_velXHash),
            z = _animator.GetFloat(_velZHash)
        };

        var smoothAnimationDirection
            = Vector3.Lerp(currentAnimationDirection, newAnimationDirection, Time.deltaTime * transitionSpeed);
        return smoothAnimationDirection;
    }
}