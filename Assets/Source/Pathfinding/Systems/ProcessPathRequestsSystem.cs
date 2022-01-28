﻿using System.Collections.Generic;
using Entitas;
using Roy_T.AStar.Paths;
using Roy_T.AStar.Primitives;
using UnityEngine;

public class ProcessPathRequestsSystem : ReactiveSystem<GameEntity>
{
    private readonly Contexts   _contexts;
    private readonly PathFinder _pathfinder = new PathFinder();

    public ProcessPathRequestsSystem(Contexts contexts) : base(contexts.game)
    {
        _contexts = contexts;
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.PathRequest.Added());
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.hasPathRequest;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        var pathFindingGrid = _contexts.game.pathfindingGrid.value;

        foreach (var e in entities)
        {
            var start = e.pathRequest.from.ToGridPosition();
            var end   = e.pathRequest.to.ToGridPosition();

            var waypoints = start != end
                                ? _pathfinder.FindPath(start, end, pathFindingGrid)
                                             .GetWaypointsFromPath()
                                : new List<Vector2Int> { end.ToVector2Int() };

            e.ReplacePath(0, waypoints);

            e.RemovePathRequest();
        }
    }
}