using System;
using Entitas;

public class ExecuteLookDirectionOrderSystem : IExecuteSystem
{
    private readonly IGroup<GameEntity> _entities;
    private readonly GameContext        _game;

    public ExecuteLookDirectionOrderSystem(Contexts contexts)
    {
        _game = contexts.game;
        _entities = contexts.game.GetGroup(GameMatcher.AllOf(GameMatcher.LookAtDirectionOrder,
                                                             GameMatcher.Vision));
    }

    public void Execute()
    {
        var deltaTime = _game.simulationTick.deltaTime;

        foreach (var e in _entities)
        {
            var vision       = e.vision;
            var orderedAngle = e.lookAtDirectionOrder.angle;

            var angleChange = vision.turningSpeed * deltaTime;

            if (Math.Abs(vision.directionAngle - orderedAngle) > 0.01)
            {
                vision.directionAngle =
                    AngleHelper.RotateAngleTowards(vision.directionAngle, orderedAngle, angleChange);

                e.ReplaceVision(vision.directionAngle, vision.viewingAngle, vision.distance, vision.turningSpeed);
            }
        }
    }
}