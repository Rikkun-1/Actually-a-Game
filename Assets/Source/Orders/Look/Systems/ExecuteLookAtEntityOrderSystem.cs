using Entitas;

public class ExecuteLookAtEntityOrderSystem : IExecuteSystem
{
    private readonly IGroup<GameEntity> _entities;
    private readonly GameContext        _game;

    public ExecuteLookAtEntityOrderSystem(Contexts contexts)
    {
        _game = contexts.game;
        _entities = contexts.game.GetGroup(GameMatcher.AllOf(GameMatcher.LookAtEntityOrder,
                                                             GameMatcher.Vision,
                                                             GameMatcher.WorldPosition));
    }

    public void Execute()
    {
        foreach (var e in _entities.GetEntities())
        {
            var targetEntity = _game.GetEntityWithId(e.lookAtEntityOrder.targetID);
            if (targetEntity == null)
            {
                e.RemoveLookAtEntityOrder();
                continue;
            };
            
            var currentPosition = e.worldPosition.value;
            var targetPosition  = targetEntity.worldPosition.value;
            var targetDirection = targetPosition - currentPosition;
            
            var angleDelta      = e.vision.turningSpeed * GameTime.deltaTime;

            VisionHelper.RotateEntityVisionTowards(e, targetDirection.ToAngle(), angleDelta);
        }
    }
}