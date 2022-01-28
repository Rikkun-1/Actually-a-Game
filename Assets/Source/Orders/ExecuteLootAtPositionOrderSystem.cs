using System;
using UnityEngine;
using Entitas;

public class ExecuteLootAtPositionOrderSystem : IExecuteSystem
{
    private readonly IGroup<GameEntity> _entities;
    private readonly Contexts           _contexts;

    public ExecuteLootAtPositionOrderSystem(Contexts contexts)
    {
        _contexts = contexts;
        _entities = contexts.game.GetGroup(GameMatcher.AllOf(GameMatcher.LookAtPositionOrder,
                                                             GameMatcher.Vision,
                                                             GameMatcher.WorldPosition));
    }

    public void Execute()
    {
        var deltaTime = _contexts.game.gameTick.deltaTime;
        
        foreach (var e in _entities)
        {
            var currentPosition = e.worldPosition.value;
            var targetPosition  = e.lookAtPositionOrder.position;
            var targetDirection = targetPosition - currentPosition;

            var vision  = e.vision.value;
            var rotated = vision.turningSpeed * deltaTime;

            float desiredAngle = targetDirection.ToAngle();
            
            if (Math.Abs(vision.directionAngle - desiredAngle) > 0.01)
            {
                vision.directionAngle = 
                    RotationHelper.RotateAngleTowards(vision.directionAngle, desiredAngle, (float)rotated);
                
                e.ReplaceVision(vision);
            }
        }
    }
}

public static class RotationHelper
{
    public static float RotateAngleTowards(float from, float to, float maxAngleDelta)
    {
        var current = Quaternion.Euler(0, from, 0);
        var ordered = Quaternion.Euler(0, to,          0);
        
        var newAngle = Quaternion.RotateTowards(current, ordered, maxAngleDelta).eulerAngles.y;

        return newAngle;
    }
}