using UnityEngine;
using Entitas;

public class DrawPathsSystem : IExecuteSystem
{
    readonly IGroup<GameEntity> _entities;

    public DrawPathsSystem(Contexts contexts)
    {
        _entities = contexts.game.GetGroup(GameMatcher.Path);
    }

    public void Execute()
    {
        foreach (var e in _entities)
        {
            var path = e.path.Path;

            foreach (var edge in path.Edges)
            {
                var start = new Vector3(edge.Start.Position.X, edge.Start.Position.Y);
                var end   = new Vector3(edge.End.Position.X,   edge.End.Position.Y);
                Debug.DrawLine(start, end, new Color(255, 0, 0));
            }
        }
    }
}