using UnityEngine;

[RequireComponent(typeof(MovementAnimationController))]
public class MovementAnimationUpdater : BaseVelocityListener
{
    private MovementAnimationController _movementAnimationController;
    
    private void Start()
    {
        _movementAnimationController = GetComponent<MovementAnimationController>();
    }
    
    public override void OnVelocity(GameEntity entity, Vector3 newVelocity)
    {
        _movementAnimationController.CalculateAnimationVelocity(newVelocity);
    }
}