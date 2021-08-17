using System.Linq;
using Entitas;
using UnityEngine;

public class DrawWalkableTilesSystem : IExecuteSystem
{
    private readonly Contexts _contexts;

    public DrawWalkableTilesSystem(Contexts contexts)
    {
        _contexts = contexts;
    }

    public void Execute()
    {
        var edges = _contexts.game.GetEntities(GameMatcher.Edges).ToList().SingleEntity().edges.Value;

        foreach (var edge in edges)
        {
            var start = new Vector3(edge.Start.Position.X, edge.Start.Position.Y);
            var end = new Vector3(edge.End.Position.X, edge.End.Position.Y);

            Debug.DrawLine(start, end, new Color(255, 0, 0));
        }
    }
}