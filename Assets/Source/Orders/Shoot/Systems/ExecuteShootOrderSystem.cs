using System;
using Entitas;

public class ExecuteShootOrderSystem : IExecuteSystem
{
    private readonly IGroup<GameEntity> _entities;
    private readonly GameContext        _game;

    public ExecuteShootOrderSystem(Contexts contexts)
    {
        _game = contexts.game;
        _entities = contexts.game.GetGroup(GameMatcher.AllOf(GameMatcher.ShootOrder,
                                                             GameMatcher.Vision,
                                                             GameMatcher.WorldPosition,
                                                             GameMatcher.TeamID,
                                                             GameMatcher.Weapon));
    }

    public void Execute()
    {
        foreach (var e in _entities)
        {
            switch (e.shootOrder.target.targetType)
            {
                case TargetType.Direction:
                {
                    var direction = e.shootOrder.target.direction.ToAngle360();
                    if (AimHelper.IsAimingAtTargetDirection(e, direction))
                    {
                        ShootHelper.Shoot(e, e.weapon);
                    }
                    break;
                }
                case TargetType.Position:
                {
                    var targetPosition = e.shootOrder.target.position;
                    if (AimHelper.IsAimingAtTargetPosition(e, targetPosition))
                    {
                        ShootHelper.Shoot(e, e.weapon);
                    }
                    break;
                }
                case TargetType.Entity:
                {
                    var targetEntityID = e.shootOrder.target.entityID;
                    var targetEntity   = _game.GetEntityWithId(targetEntityID);
                    if (targetEntity == null)
                    {
                        e.RemoveShootOrder();
                        continue;
                    }

                    if (AimHelper.IsAimingAtTargetEntity(e, targetEntity))
                    {
                        if (!e.hasReactionStartTime)
                        {
                            e.AddReactionStartTime(GameTime.timeFromStart);
                        }

                        var reactionIsPassed = GameTime.timeFromStart - e.reactionStartTime.value > e.reactionDelay.value;
                        if (reactionIsPassed)
                        {
                            ShootHelper.Shoot(e, e.weapon);
                        }
                    }

                    break;
                }
                default: throw new ArgumentOutOfRangeException();
            }
        }
    }
}