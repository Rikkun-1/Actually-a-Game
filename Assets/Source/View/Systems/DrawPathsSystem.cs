using Entitas;
using Roy_T.AStar.Primitives;
using UnityEngine;

public class DrawPathsSystem : IExecuteSystem
{
    private readonly IGroup<GameEntity> _entities;

    public DrawPathsSystem(Contexts contexts)
    {
        _entities = contexts.game.GetGroup(GameMatcher.Path);
    }

    public void Execute()
    {
        foreach (var e in _entities)
        {
            var waypoints = e.path.waypoints;

            for (int i = 0; i < waypoints.Count - 1; i++)
            {
                var start = waypoints[i].ToVector3();
                var end   = waypoints[i+1].ToVector3();
                Debug.DrawLine(start, end, new Color(255, 0, 0));
            }
        }
    }
}