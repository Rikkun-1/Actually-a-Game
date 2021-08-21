using System.Linq;
using Entitas;
using Roy_T.AStar.Paths;
using Roy_T.AStar.Primitives;
using UnityEngine;

namespace Source.TestSystems
{
    public class TestPathSystem : IInitializeSystem, IExecuteSystem
    {
        private readonly Contexts   _contexts;
        private readonly PathFinder _pathFinder = new PathFinder();

        private GameEntity _endPos;
        private GameEntity _startPos;

        public TestPathSystem(Contexts contexts)
        {
            _contexts = contexts;
        }

        public void Execute()
        {
            var gridHolder = _contexts.game.GetEntities(GameMatcher.PathfindingGrid)
                                      .ToList()
                                      .SingleEntity();

            if (gridHolder != null)
            {
                var startPos = _startPos.position.Value;
                var endPos   = _endPos.position.Value;
                var path = _pathFinder.FindPath(new GridPosition(startPos.x, startPos.y),
                                                new GridPosition(endPos.x,   endPos.y),
                                                gridHolder.pathfindingGrid.Value);

                Debug.Log($"type: {path.Type}, distance: {path.Distance}, duration {path.Duration}");

                foreach (var edge in path.Edges)
                {
                    var start = new Vector3(edge.Start.Position.X, edge.Start.Position.Y);
                    var end   = new Vector3(edge.End.Position.X,   edge.End.Position.Y);
                    Debug.DrawLine(start, end, new Color(255, 0, 0));
                }
            }
        }

        public void Initialize()
        {
            _startPos = _contexts.game.CreateEntity();
            _endPos   = _contexts.game.CreateEntity();

            _startPos.AddPosition(new Vector2Int(2, 2));
            _endPos.AddPosition(new Vector2Int(5,   5));

            _startPos.isIndestructible = true;
            _endPos.isIndestructible   = true;
        }
    }
}