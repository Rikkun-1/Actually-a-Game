using Entitas;

public class ExecuteLookDirectionOrderSystem : IExecuteSystem
{
    private readonly IGroup<GameEntity> _entities;

    public ExecuteLookDirectionOrderSystem(Contexts contexts)
    {
        _entities = contexts.game.GetGroup(GameMatcher.AllOf(GameMatcher.LookAtDirectionOrder,
                                                             GameMatcher.Vision));
    }

    public void Execute()
    {
        foreach (var e in _entities)
        {
            var desiredAngle = e.lookAtDirectionOrder.angle;
            var angleDelta   = e.vision.turningSpeed * GameTime.deltaTime;
            VisionHelper.RotateEntityVisionTowards(e, desiredAngle, angleDelta);
        }
    }
}