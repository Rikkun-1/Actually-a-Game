using System;
using Entitas;

public class ExecuteLookDirectionOrderSystem : IExecuteSystem
{
    private readonly Contexts           _contexts;
    private readonly IGroup<GameEntity> _entities;

    public ExecuteLookDirectionOrderSystem(Contexts contexts)
    {
        _contexts = contexts;
        _entities = contexts.game.GetGroup(GameMatcher.AllOf(GameMatcher.LookAtDirectionOrder,
                                                             GameMatcher.Vision));
    }

    public void Execute()
    {
        var deltaTime = _contexts.game.gameTick.deltaTime;

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