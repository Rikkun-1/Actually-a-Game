using Entitas;
using ProceduralToolkit;

public class ExecuteLootAtPositionOrderSystem : IExecuteSystem
{
    private readonly IGroup<GameEntity> _entities;

    public ExecuteLootAtPositionOrderSystem(Contexts contexts)
    {
        _entities = contexts.game.GetGroup(GameMatcher.AllOf(GameMatcher.LookAtPositionOrder,
                                                             GameMatcher.Vision,
                                                             GameMatcher.WorldPosition));
    }

    public void Execute()
    {
        foreach (var e in _entities)
        {
            var currentPosition = e.worldPosition.value;
            var targetPosition  = e.lookAtPositionOrder.position.ToVector3XZ();
            var targetDirection = targetPosition - currentPosition;

            var angleDelta = e.vision.turningSpeed * GameTime.deltaTime;

            VisionHelper.RotateEntityVisionTowards(e, targetDirection.ToVector2XZ().ToAngle(), angleDelta);
        }
    }
}