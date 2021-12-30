using System.Collections.Generic;
using System.Linq;
using Entitas;
using UnityEngine;

public class ShootAtEnemyInSightSystem : IExecuteSystem
{
    private readonly IGroup<GameEntity> _entities;
    private readonly IGroup<GameEntity> _possibleTargets;

    public ShootAtEnemyInSightSystem(Contexts contexts)
    {
        _entities = contexts.game.GetGroup(GameMatcher.AllOf(GameMatcher.TeamID,
                                                             GameMatcher.Vision)
                                                      .NoneOf(GameMatcher.ShootAtEntityOrder));

        _possibleTargets = contexts.game.GetGroup(GameMatcher.AllOf(GameMatcher.TeamID));
    }

    public void Execute()
    {
        foreach (var e in _entities.GetEntities())
        {
            var possibleTargetsByDistance = GetPossibleTargetsByDistance(e);
            foreach (var targetEntity in possibleTargetsByDistance)
            {
                if (RaycastHelper.IsInClearVision(e, targetEntity))
                {
                    e.ReplaceShootAtEntityOrder(targetEntity.id.value);
                    break;
                }
            }
        }
    }

    private static bool IsInSameTeam(GameEntity e, GameEntity targetEntity)
    {
        return e.hasTeamID &&
               targetEntity.hasTeamID &&
               e.teamID.value == targetEntity.teamID.value;
    }

    private IEnumerable<GameEntity> GetPossibleTargetsByDistance(GameEntity e)
    {
        return _possibleTargets.GetEntities()
                               .Where(targetEntity => !IsInSameTeam(e, targetEntity))
                               .OrderBy(entity => Vector2.Distance(e.worldPosition.value, entity.worldPosition.value));
    }
}