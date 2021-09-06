using System;
using Entitas;
using UnityEngine;

public class ExecuteLookDirectionOrderSystem : IExecuteSystem
{
    readonly         IGroup<GameEntity> _entities;
    private readonly Contexts           _contexts;

    public ExecuteLookDirectionOrderSystem(Contexts contexts)
    {
        _contexts = contexts;
        _entities = _contexts.game.GetGroup(GameMatcher.AllOf(GameMatcher.LookDirectionOrder,
                                                              GameMatcher.Vision));
    }

    public void Execute()
    {
        var deltaTime = _contexts.game.gameTick.deltaTime;
        
        foreach (var e in _entities)
        {
            var vision       = e.vision.value;
            var orderedAngle = e.lookDirectionOrder.angle;
            
            if (Math.Abs(vision.directionAngle - orderedAngle) > 0.1)
            {
                var current = Quaternion.Euler(0, vision.directionAngle, 0);
                var ordered = Quaternion.Euler(0, orderedAngle,          0);

                float degrees = vision.turningSpeed * (float)deltaTime;
                
                var newAngle = Quaternion.RotateTowards(current, ordered, degrees).eulerAngles.y;

                vision.directionAngle = newAngle;
                e.ReplaceVision(vision);
            }
        }
    }
}