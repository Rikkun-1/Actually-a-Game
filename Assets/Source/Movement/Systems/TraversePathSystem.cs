using System.Collections.Generic;
using System.Linq;
using Entitas;
using UnityEngine;

public class TraversePathSystem : IExecuteSystem
{
    private readonly GameContext        _game;
    private readonly IGroup<GameEntity> _entities;

    public TraversePathSystem(Contexts contexts)
    {
        _game = contexts.game;
        _entities = contexts.game.GetGroup(GameMatcher.AllOf(GameMatcher.WorldPosition,
                                                             GameMatcher.TraversalSpeed,
                                                             GameMatcher.Path));
    }

    public void Execute()
    {
        foreach (var e in _entities.GetEntities())
        {
            TraversePath(e);
        }
    }

    private void TraversePath(GameEntity e)
    {
        var distanceTraveledLeft = SkipWaypoints(e);
        var nextWaypoint         = e.path.waypoints[e.path.currentIndex];

        if (IsOccupiedByAnotherPlayer(e, nextWaypoint)) return;
        
        var newWorldPosition = Vector2.MoveTowards(e.worldPosition.value,
                                                   nextWaypoint,
                                                   distanceTraveledLeft);

        e.ReplaceWorldPosition(newWorldPosition);
        
        CheckIfStoppedAtWaypoint(e, nextWaypoint);
        RemovePathIfOnLastCheckpoint(e);
    }

    private bool IsOccupiedByAnotherPlayer(GameEntity e, Vector2Int nextWaypoint)
    {
        return _game.GetEntitiesWithGridPosition(nextWaypoint)
                    .Any((entity) => entity.isPlayer && entity.id.value != e.id.value);
    }

    private static float SkipWaypoints(GameEntity e)
    {
        var distanceTraveled = e.traversalSpeed.value * GameTime.deltaTime;
        var currentIndex     = e.path.currentIndex;
        var waypoints        = e.path.waypoints;

        var worldPosition = e.worldPosition.value;
        var nextWaypoint  = waypoints[currentIndex];
        var distanceToNextWaypoint = Vector2.Distance(worldPosition, nextWaypoint);

        while (currentIndex + 1 < waypoints.Count && distanceToNextWaypoint < distanceTraveled)
        {
            worldPosition          =  nextWaypoint;
            distanceTraveled       -= distanceToNextWaypoint;
            currentIndex           += 1;
            nextWaypoint           =  waypoints[currentIndex];
            distanceToNextWaypoint =  Vector2.Distance(worldPosition, nextWaypoint);
        }
        
        e.ReplaceWorldPosition(worldPosition);
        UpdateCurrentWaypointIndex(e, currentIndex);
        return distanceTraveled;
    }

    private static void CheckIfStoppedAtWaypoint(GameEntity e, Vector2Int nextWaypoint)
    {
        if(e.worldPosition.value == nextWaypoint)
        {
            UpdateCurrentWaypointIndex(e, e.path.currentIndex + 1);
        }
    }

    private static void RemovePathIfOnLastCheckpoint(GameEntity e)
    {
        var waypoints = e.path.waypoints; 
        if (waypoints.Count == 0 || e.worldPosition.value == waypoints.Last())
        {
            e.RemovePath();
        }
    }
    
    private static void UpdateCurrentWaypointIndex(GameEntity e, int currentIndex)
    {
        if (currentIndex != e.path.currentIndex)
        {
            e.ReplacePath(currentIndex, e.path.waypoints);
        }
    }
}