using System;
using Entitas;
using UnityEngine;

public class ExecuteLookDirectionOrderSystem : IExecuteSystem
{
    private readonly IGroup<GameEntity> _entities;
    private readonly Contexts           _contexts;

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
            var vision       = e.vision.value;
            var orderedAngle = e.lookAtDirectionOrder.angle;
            
            var rotated = vision.turningSpeed * deltaTime;

            if (Math.Abs(vision.directionAngle - orderedAngle) > 0.01)
            {
                vision.directionAngle = 
                    RotationHelper.RotateAngleTowards(vision.directionAngle, orderedAngle, (float)rotated);

                e.ReplaceVision(vision);
            }
        }
    }
}