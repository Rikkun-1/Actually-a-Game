using System;
using Entitas;
using UnityEngine;

public class ExecuteLookAtEntityOrderSystem : IExecuteSystem
{
    private readonly Contexts           _contexts;
    private readonly IGroup<GameEntity> _entities;

    public ExecuteLookAtEntityOrderSystem(Contexts contexts)
    {
        _contexts = contexts;
        _entities = contexts.game.GetGroup(GameMatcher.AllOf(GameMatcher.LookAtEntityOrder,
                                                             GameMatcher.Vision,
                                                             GameMatcher.WorldPosition));
    }

    public void Execute()
    {
        var deltaTime = _contexts.game.gameTick.deltaTime;

        foreach (var e in _entities)
        {
            var currentPosition = e.worldPosition.value;

            var targetEntityID = e.lookAtEntityOrder.targetID;
            var targetEntity   = _contexts.game.GetEntityWithID(targetEntityID);
            if (targetEntity == null) continue;
            
            var targetPosition = targetEntity.worldPosition.value;

            var targetDirection = targetPosition - currentPosition;

            var vision      = e.vision.value;
            var angleChange = vision.turningSpeed * deltaTime;

            var desiredAngle = targetDirection.ToAngle();

            if (Math.Abs(vision.directionAngle - desiredAngle) > 0.01)
            {
                vision.directionAngle =
                    AngleHelper.RotateAngleTowards(vision.directionAngle, desiredAngle, angleChange);

                e.ReplaceVision(vision);
            }
        }
    }
}