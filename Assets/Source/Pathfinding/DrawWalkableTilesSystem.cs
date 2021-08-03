using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entitas;

public class DrawWalkableTilesSystem : IExecuteSystem
{
    readonly Contexts contexts;

    public DrawWalkableTilesSystem(Contexts contexts)
    {
        this.contexts = contexts;
    }

    public void Execute()
    {
        var edges = this.contexts.game.GetEntities(GameMatcher.Edges).ToList().SingleEntity().edges.value;

        foreach (var edge in edges)
        {
            var start = new Vector3(edge.Start.Position.X, edge.Start.Position.Y);
            var end   = new Vector3(edge.End.Position.X, edge.End.Position.Y);

            Debug.DrawLine(start, end, new Color(255, 0, 0));
        }
    }
}