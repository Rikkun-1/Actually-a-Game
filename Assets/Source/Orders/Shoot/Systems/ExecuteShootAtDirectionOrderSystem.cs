using System;
using Entitas;

public class ExecuteShootAtDirectionOrderSystem : IExecuteSystem
{
    private readonly Contexts           _contexts;
    private readonly IGroup<GameEntity> _entities;
    private readonly IGroup<GameEntity> _entitiesThatDontLookAtProperDirection;

    public ExecuteShootAtDirectionOrderSystem(Contexts contexts)
    {
        _contexts = contexts;
        _entities = contexts.game.GetGroup(GameMatcher.AllOf(GameMatcher.ShootAtDirectionOrder,
                                                             GameMatcher.Vision,
                                                             GameMatcher.WorldPosition));

        _entitiesThatDontLookAtProperDirection = contexts.game.GetGroup(GameMatcher.AllOf(GameMatcher.ShootAtDirectionOrder,
                                                                                          GameMatcher.Vision,
                                                                                          GameMatcher.WorldPosition)
                                                                                   .NoneOf(GameMatcher.LookAtDirectionOrder));
    }

    public void Execute()
    {
        foreach (var e in _entitiesThatDontLookAtProperDirection.GetEntities())
        {
            e.AddLookAtDirectionOrder(e.shootAtDirectionOrder.angle);
        }

        foreach (var e in _entities)
        {
            if (Math.Abs(e.lookAtDirectionOrder.angle - e.shootAtDirectionOrder.angle) > 0.01)
            {
                e.ReplaceLookAtDirectionOrder(e.shootAtDirectionOrder.angle);
            }

            var direction = e.shootAtDirectionOrder.angle;
            if (AimHelper.IsAimingAtTargetDirection(e, direction))
            {
                ShootHelper.Shoot(e.worldPosition.value, e.vision.value.directionAngle, e.weapon.value, e.iD.value);
            }
        }
    }
}