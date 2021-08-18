using System.Linq;
using Entitas;
using Roy_T.AStar.Paths;
using Roy_T.AStar.Primitives;
using UnityEngine;

namespace Source.TestSystems
{
    public class TestPathSystem : IInitializeSystem, IExecuteSystem
    {
        private readonly Contexts _contexts;
        private readonly PathFinder pathFinder = new PathFinder();

        private GameEntity EndPos;
        private GameEntity StartPos;

        public TestPathSystem(Contexts contexts)
        {
            _contexts = contexts;
        }

        public void Execute()
        {
            var _gridHolder = _contexts.game.GetEntities(GameMatcher.PathfindingGrid)
                .ToList()
                .SingleEntity();

            if (_gridHolder != null)
            {
                var startPos = StartPos.position.Value;
                var endPos = EndPos.position.Value;
                var path = pathFinder.FindPath(new GridPosition(startPos.x, startPos.y),
                    new GridPosition(endPos.x, endPos.y), _gridHolder.pathfindingGrid.Value);

                Debug.Log($"type: {path.Type}, distance: {path.Distance}, duration {path.Duration}");

                foreach (var edge in path.Edges)
                {
                    var start = new Vector3(edge.Start.Position.X, edge.Start.Position.Y);
                    var end = new Vector3(edge.End.Position.X, edge.End.Position.Y);
                    Debug.DrawLine(start, end, new Color(255, 0, 0));
                }
            }
        }

        public void Initialize()
        {
            StartPos = _contexts.game.CreateEntity();
            EndPos = _contexts.game.CreateEntity();
            StartPos.AddPosition(new Vector2Int(2, 2));
            EndPos.AddPosition(new Vector2Int(5, 5));
            StartPos.isUndestructible = true;
            EndPos.isUndestructible = true;
        }
    }
}