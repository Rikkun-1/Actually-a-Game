using System.Linq;
using Entitas;
using GraphProcessor;
using ProceduralToolkit;
using UnityEngine;

public class ProcessAISystem : IInitializeSystem, IExecuteSystem
{
    private readonly IGroup<GameEntity>    _entities;
    private readonly GameContext           _game;
    private          BaseGraph             _baseGraph;
    private          ProcessGraphProcessor _processor;

    public ProcessAISystem(Contexts contexts)
    {
        _game     = contexts.game;
        _entities = contexts.game.GetGroup(GameMatcher.AllOf(GameMatcher.TeamID, GameMatcher.AI));
    }
    
    public void Initialize()
    {
        _baseGraph = _game.aIGraph.graph;
        _processor = new ProcessGraphProcessor(_baseGraph);
    }

    public void Execute()
    {
        CacheLambdaResult.Cache.Clear();
        foreach (var e in _entities)
        {
            var moveTarget = HandleMovement(e);
            if (!e.hasShootOrder) HandleAiming(e, moveTarget);
        }
    }

    private Vector2Int HandleMovement(GameEntity e)
    {
        _baseGraph.SetParameterValue("Entity ID", e.id.value);
        _processor.Run();

        var x = _baseGraph.GetParameterValue<int>("SolutionX");
        var y = _baseGraph.GetParameterValue<int>("SolutionY");

        var moveTarget = new Vector2Int(x, y);
        e.ReplaceMoveToPositionOrder(moveTarget);
        return moveTarget;
    }

    private void HandleAiming(GameEntity e, Vector2 moveTarget)
    {
        var enemyPositions = GetEnemyPositions(e, moveTarget);
        if (enemyPositions.Length == 0) return;
        
        var positionToLookAt = Vector2IntExtensions.Average(enemyPositions);
        var target           = Target.Position(positionToLookAt);
        
        e.ReplaceAITarget(target);
        e.ReplaceLookOrder(target);
    }

    private Vector2Int[] GetEnemyPositions(GameEntity e, Vector2 moveTarget)
    {
        var enemyTeamIDs = TeamIDHelper.GetEnemyTeamIDs(_game, e.teamID.value);

        var enemies = enemyTeamIDs.Select(teamID => _game.GetEntitiesWithTeamID(teamID))
                                  .SelectMany(enemy => enemy)
                                  .ToList();

        var origin         = moveTarget.ToVector3XZ().WithY(1.6f);
        var visibleEnemies = enemies.Where(enemy => RaycastHelper.IsInClearVision(origin, enemy));
        var enemyPositions = visibleEnemies.Select(enemy => enemy.gridPosition.value).ToArray();
        return enemyPositions;
    }
}