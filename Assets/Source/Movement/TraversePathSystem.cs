﻿using System.Linq;
using Entitas;
using UnityEngine;

public class TraversePathSystem : IExecuteSystem
{
    private readonly IGroup<GameEntity> _entities;
    private readonly GameContext        _game;

    public TraversePathSystem(Contexts contexts)
    {
        _game = contexts.game;
        _entities = contexts.game.GetGroup(GameMatcher.AllOf(GameMatcher.WorldPosition,
                                                             GameMatcher.TraversalSpeed,
                                                             GameMatcher.Path));
    }

    public void Execute()
    {
        var deltaTime = _game.simulationTick.deltaTime;

        foreach (var e in _entities.GetEntities())
        {
            var currentIndex = e.path.currentIndex;
            var waypoints    = e.path.waypoints;

            var distanceTraveled = e.traversalSpeed.value * deltaTime;

            var worldPosition          = e.worldPosition.value;
            var nextWaypoint           = waypoints[currentIndex];
            var distanceToNextWaypoint = Vector2.Distance(worldPosition, nextWaypoint);

            while (currentIndex + 1 < waypoints.Count && distanceToNextWaypoint < distanceTraveled)
            {
                worldPosition          =  nextWaypoint;
                distanceTraveled       -= distanceToNextWaypoint;
                currentIndex           += 1;
                nextWaypoint           =  waypoints[currentIndex];
                distanceToNextWaypoint =  Vector2.Distance(worldPosition, nextWaypoint);
            }

            worldPosition = Vector2.MoveTowards(worldPosition,
                                                nextWaypoint,
                                                distanceTraveled);

            e.ReplaceWorldPosition(worldPosition);

            if (worldPosition == waypoints[currentIndex])
            {
                currentIndex += 1;
            }

            if (currentIndex != e.path.currentIndex)
            {
                e.ReplacePath(currentIndex, e.path.waypoints);
            }

            if (worldPosition == waypoints.Last())
            {
                e.RemovePath();
            }
        }
    }
}