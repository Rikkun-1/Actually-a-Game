using System;
using Entitas;
using ProceduralToolkit;
using UnityEngine;

public class ExecuteShootAtEntityOrderSystem : IExecuteSystem
{
    private readonly Contexts           _contexts;
    private readonly IGroup<GameEntity> _entities;
    private readonly IGroup<GameEntity> _entitiesThatDontLookAtProperEntity;

    public ExecuteShootAtEntityOrderSystem(Contexts contexts)
    {
        _contexts = contexts;
        _entities = contexts.game.GetGroup(GameMatcher.AllOf(GameMatcher.ShootAtEntityOrder,
                                                             GameMatcher.Vision,
                                                             GameMatcher.WorldPosition));

        _entitiesThatDontLookAtProperEntity = contexts.game.GetGroup(GameMatcher.AllOf(GameMatcher.ShootAtEntityOrder,
                                                                                       GameMatcher.Vision,
                                                                                       GameMatcher.WorldPosition)
                                                                                .NoneOf(GameMatcher.LookAtEntityOrder));
    }

    public void Execute()
    {
        foreach (var e in _entitiesThatDontLookAtProperEntity.GetEntities())
        {
            e.AddLookAtEntityOrder(e.shootAtEntityOrder.targetID);
        }

        foreach (var e in _entities)
        {
            if (e.lookAtEntityOrder.targetID != e.shootAtEntityOrder.targetID)
            {
                e.ReplaceLookAtEntityOrder(e.shootAtEntityOrder.targetID);
            }
            
            var targetEntityID = e.shootAtEntityOrder.targetID;
            var targetEntity   = _contexts.game.GetEntityWithID(targetEntityID);
            
            if (AimHelper.IsAimingAtTargetEntity(e, targetEntity))
            {
                ShootHelper.Shoot(e.worldPosition.value, e.vision.value.directionAngle, e.weapon.value);
            }
        }
    }
}