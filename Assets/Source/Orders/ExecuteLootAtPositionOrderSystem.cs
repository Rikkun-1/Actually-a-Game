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

            var vision = e.vision.value;

            float desiredAngle = Mathf.Atan2( targetDirection.x, targetDirection.y )  * Mathf.Rad2Deg; // Assuming you want degrees not radians?
            
            if (Math.Abs(vision.directionAngle - desiredAngle) > 0.01)
            {
                var current = Quaternion.Euler(0, vision.directionAngle, 0);
                var ordered = Quaternion.Euler(0, desiredAngle,          0);

                float degrees = vision.turningSpeed * (float)deltaTime;
                
                var newAngle = Quaternion.RotateTowards(current, ordered, degrees).eulerAngles.y;

                vision.directionAngle = newAngle;
                e.ReplaceVision(vision);
            }
        }
    }
}