using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entitas;
using UnityEngine;

public class ShootAtEnemyInSightSystem : IExecuteSystem
{
    private readonly IGroup<GameEntity> _entities;
    private readonly IGroup<GameEntity> _possibleTargets;

    private IEnumerable<GameEntity>[] _possibleTargetsByDistance = Array.Empty<IEnumerable<GameEntity>>();

    private int _delayBetweenChecks = 15;
    private int _frame;

    public ShootAtEnemyInSightSystem(Contexts contexts)
    {
        _entities = contexts.game.GetGroup(GameMatcher.AllOf(GameMatcher.TeamID,
                                                             GameMatcher.Vision)
                                                      .NoneOf(GameMatcher.ShootOrder));

        _possibleTargets = contexts.game.GetGroup(GameMatcher.AllOf(GameMatcher.TeamID)
                                                             .NoneOf(GameMatcher.Destroyed));
    }

    public void Execute()
    {
        _frame++;
        if (_frame == _delayBetweenChecks)
        {
            _frame = 0;
            FindTargets();
        }
    }

    private void FindTargets()
    {
        var entities = _entities.GetEntities();
        var length   = entities.Length;

        FindPossibleTargets(length, entities);

        for (var i = 0; i < length; i++)
        {
            var e = entities[i];

            var possibleTargetsByDistance = _possibleTargetsByDistance[i];
            foreach (var targetEntity in possibleTargetsByDistance)
            {
                if (RaycastHelper.IsInClearVision(e, targetEntity))
                {
                    e.ReplaceShootOrder(Target.Entity(targetEntity.id.value));
                    break;
                }
            }
        }
    }

    private void FindPossibleTargets(int length, GameEntity[] entities)
    {
        if (_possibleTargetsByDistance.Length < length) _possibleTargetsByDistance = new IEnumerable<GameEntity>[length];
        Parallel.For(0L, length, (i) => _possibleTargetsByDistance[i] = GetPossibleTargetsByDistance(entities[i]));
    }

    private static bool IsInSameTeam(GameEntity e, GameEntity targetEntity)
    {
        return e.teamID.value == targetEntity.teamID.value;
    }

    private IEnumerable<GameEntity> GetPossibleTargetsByDistance(GameEntity e)
    {
        return _possibleTargets.GetEntities()
                               .Where(targetEntity => !IsInSameTeam(e, targetEntity) && Vector2.Distance(e.gridPosition.value, targetEntity.gridPosition.value) < 50f)
                               .OrderBy(entity => Vector2.Distance(e.gridPosition.value, entity.gridPosition.value));
    }
}