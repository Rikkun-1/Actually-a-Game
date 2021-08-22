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
            var path = e.path.path;

            foreach (var edge in path.Edges)
            {
                var start = edge.Start.Position.ToVector3();
                var end   = edge.End.Position.ToVector3();
                Debug.DrawLine(start, end, new Color(0, 255, 0));
            }
        }
    }
}