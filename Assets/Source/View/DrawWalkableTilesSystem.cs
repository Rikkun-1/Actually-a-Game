using Entitas;
using Roy_T.AStar.Primitives;
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
        if (_contexts.game.hasPathfindingGrid)
        {
            var edges = _contexts.game.pathfindingGrid.value.GetAllEdges();

            foreach (var edge in edges)
            {
                var start = edge.Start.Position.ToVector3();
                var end   = edge.End.Position.ToVector3();

                Debug.DrawLine(start, end, new Color(255, 0, 0));
            }
        }
    }
}