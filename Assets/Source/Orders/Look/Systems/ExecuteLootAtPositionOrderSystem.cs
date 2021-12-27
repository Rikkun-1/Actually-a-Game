using System;
using Entitas;

public class ExecuteLootAtPositionOrderSystem : IExecuteSystem
{
    private readonly IGroup<GameEntity> _entities;
    private readonly GameContext        _game;

    public ExecuteLootAtPositionOrderSystem(Contexts contexts)
    {
        _game = contexts.game;
        _entities = contexts.game.GetGroup(GameMatcher.AllOf(GameMatcher.LookAtPositionOrder,
                                                             GameMatcher.Vision,
                                                             GameMatcher.WorldPosition));
    }

    public void Execute()
    {
        var deltaTime = _game.simulationTick.deltaTime;

        foreach (var e in _entities)
        {
            var currentPosition = e.worldPosition.value;
            var targetPosition  = e.lookAtPositionOrder.position;
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