using Entitas;
using ProceduralToolkit;
using UnityEngine;

public class RotateTowardsMovementDirectionSystem : IExecuteSystem
{
    private readonly IGroup<GameEntity> _entities;

    public RotateTowardsMovementDirectionSystem(Contexts contexts)
    {
        _entities = contexts.game.GetGroup(GameMatcher.AllOf(GameMatcher.Velocity,
                                                             GameMatcher.Vision)
                                                      .NoneOf(GameMatcher.LookOrder,
                                                              GameMatcher.AITarget,
                                                              GameMatcher.Destroyed));
    }

    public void Execute()
    {
        foreach (var e in _entities)
        {
            if (e.velocity.value == Vector3.zero) return;
            var desiredAngle  = e.velocity.value.ToVector2XZ().ToAngle360();
            var angleDelta    = e.vision.turningSpeed * GameTime.deltaTime;
            VisionHelper.RotateEntityVisionTowards(e, desiredAngle, angleDelta);
        }
    }
}