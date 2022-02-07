using Entitas;
using ProceduralToolkit;

public class RotateTowardsMovementDirectionSystem : IExecuteSystem
{
    private readonly IGroup<GameEntity> _entities;

    public RotateTowardsMovementDirectionSystem(Contexts contexts)
    {
        _entities = contexts.game.GetGroup(GameMatcher.AllOf(GameMatcher.Velocity,
                                                             GameMatcher.Vision)
                                                      .NoneOf(GameMatcher.LookAtPositionOrder,
                                                              GameMatcher.LookAtDirectionOrder,
                                                              GameMatcher.LookAtEntityOrder,
                                                              GameMatcher.Destroyed));
    }

    public void Execute()
    {
        foreach (var e in _entities)
        {
            var desiredAngle  = e.velocity.value.ToVector2XZ().ToAngle();
            var angleDelta    = e.vision.turningSpeed * GameTime.deltaTime;
            VisionHelper.RotateEntityVisionTowards(e, desiredAngle, angleDelta);
        }
    }
}