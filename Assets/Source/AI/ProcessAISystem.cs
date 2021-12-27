using System.Collections.Generic;
using System.Linq;
using Entitas;
using UnityEngine;

public class ProcessAISystem : IExecuteSystem
{
    private readonly IGroup<GameEntity> _entities;
    private readonly GameContext        _game;

    public ProcessAISystem(Contexts contexts)
    {
        _game     = contexts.game;
        _entities = contexts.game.GetGroup(GameMatcher.AllOf(GameMatcher.TeamID, GameMatcher.AI));
    }

    public void Execute()
    {
        var teamIDs = new List<int>();
        teamIDs.AddRange(_entities.GetEntities().Select(e => e.teamID.value).Distinct());

        foreach (var e in _entities)
        {
            var enemyTeamIDs = new List<int>();
            enemyTeamIDs.AddRange(teamIDs.FindAll(id => id != e.teamID.value));

            var amountOfPossibleEnemiesMap =
                TacticalMapCreator.CreateAmountOfTeamPlayersThatCanBeSeenFromThisPositionMap(_game, enemyTeamIDs[0]);

            var distanceToPositionsMap =
                TacticalMapCreator.CreateDistanceFromThisPositionToAllPositionsMap(_game, e.worldPosition.value.ToVector2Int());

            var positionsWhereICanShootSomebody = amountOfPossibleEnemiesMap >= 1;

            var distanceToPositionsWhereICanShootSomebody = positionsWhereICanShootSomebody * distanceToPositionsMap;

            var solution = distanceToPositionsWhereICanShootSomebody.Min(x => x > 0);

            var pos = new Vector2Int(solution.x, solution.y);
            e.ReplaceMoveToPositionOrder(pos);
        }
    }
}