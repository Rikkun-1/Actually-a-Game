using System;
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
        var deltaTime = _game.simulationTick.deltaTime;

        foreach (var e in _entities)
        {
            var currentPosition = e.worldPosition.value;

            var targetEntityID = e.lookAtEntityOrder.targetID;
            var targetEntity   = _game.GetEntityWithId(targetEntityID);
            if (targetEntity == null) continue;

            var targetPosition = targetEntity.worldPosition.value;

            var targetDirection = targetPosition - currentPosition;

            var vision      = e.vision;
            var angleChange = vision.turningSpeed * deltaTime;

            var desiredAngle = targetDirection.ToAngle();

            if (Math.Abs(vision.directionAngle - desiredAngle) > 0.01)
            {
                vision.directionAngle =
                    AngleHelper.RotateAngleTowards(vision.directionAngle, desiredAngle, angleChange);

                e.ReplaceVision(vision.directionAngle, vision.viewingAngle, vision.distance, vision.turningSpeed);
            }
        }
    }
}