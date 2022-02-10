using Entitas;
using UnityEngine;

public class DrawWalkableTilesSystem : IExecuteSystem
{
    private readonly GameContext _game;

    public DrawWalkableTilesSystem(Contexts contexts)
    {
        _game = contexts.game;
    }

    public void Execute()
    {
        if (_game.hasPathfindingGrid)
        {
            var edges = _game.pathfindingGrid.value.GetAllEdges();

            foreach (var edge in edges)
            {
                var start = edge.Start.Position.ToVector3XZ();
                var end   = edge.End.Position.ToVector3XZ();

                Debug.DrawLine(start, end, new Color(0, 255, 0));
            }
        }
    }
}