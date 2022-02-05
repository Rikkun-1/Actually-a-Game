﻿using Entitas;

public class ExecuteShootAtDirectionOrderSystem : IExecuteSystem
{
    private readonly IGroup<GameEntity> _entities;

    public ExecuteShootAtDirectionOrderSystem(Contexts contexts)
    {
        _entities = contexts.game.GetGroup(GameMatcher.AllOf(GameMatcher.ShootAtDirectionOrder,
                                                             GameMatcher.Vision,
                                                             GameMatcher.WorldPosition,
                                                             GameMatcher.TeamID,
                                                             GameMatcher.Weapon));
    }

    public void Execute()
    {
        foreach (var e in _entities)
        {
            var direction = e.shootAtDirectionOrder.direction.ToAngle();
            if (AimHelper.IsAimingAtTargetDirection(e, direction))
            {
                ShootHelper.Shoot(e, e.weapon);
            }
        }
    }
}