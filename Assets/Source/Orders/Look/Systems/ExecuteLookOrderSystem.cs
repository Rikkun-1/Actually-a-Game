using System;
using Entitas;
using ProceduralToolkit;

public class ExecuteLookOrderSystem : IExecuteSystem
{
    private readonly GameContext        _game;
    private readonly IGroup<GameEntity> _entities;

    public ExecuteLookOrderSystem(Contexts contexts)
    {
        _game = contexts.game;
        _entities = contexts.game.GetGroup(GameMatcher.AllOf(GameMatcher.LookOrder,
                                                             GameMatcher.Vision,
                                                             GameMatcher.WorldPosition));
    }

    public void Execute()
    {
        foreach (var e in _entities)
        {
            float desiredAngle;
            switch (e.lookOrder.target.targetType)
            {
                case TargetType.Direction:
                {
                    desiredAngle = e.lookOrder.target.direction.ToAngle360();
                    break;
                }
                case TargetType.Position:
                {
                    var currentPosition = e.worldPosition.value.ToVector2XZ();
                    var targetPosition  = e.lookOrder.target.position;
                    desiredAngle = Vector2Extensions.Angle360(currentPosition, targetPosition);
                    break;
                }
                case TargetType.Entity:
                {
                    var targetEntity = _game.GetEntityWithId(e.lookOrder.target.entityID);
                    if (targetEntity == null)
                    {
                        e.RemoveLookOrder();
                        continue;
                    }
        
                    var currentPosition = e.worldPosition.value.ToVector2XZ();
                    var targetPosition  = targetEntity.worldPosition.value.ToVector2XZ();

                    desiredAngle = Vector2Extensions.Angle360(currentPosition, targetPosition);
                    break;
                }
                default: throw new ArgumentOutOfRangeException();
            }

            var angleDelta = e.vision.turningSpeed * GameTime.deltaTime;
            VisionHelper.RotateEntityVisionTowards(e, desiredAngle, angleDelta);
        }
    }
}