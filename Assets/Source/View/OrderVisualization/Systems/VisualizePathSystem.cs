using System.Collections.Generic;
using System.Linq;
using Entitas;
using ProceduralToolkit;
using UnityEngine;

public class VisualizePathSystem : VisualizeOrderSystemBase, IInitializeSystem
{
    private readonly GameContext _game;

    private GameObject _visualizationPrefab;

    public VisualizePathSystem(Contexts contexts) : base(contexts.game)
    {
        _game = contexts.game;
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.Path.AddedOrRemoved());
    }

    protected override bool Filter(GameEntity entity)
    {
        return true;
    }

    public void Initialize()
    {
        _visualizationPrefab = _game.gameSettings.value.orderVisualizationPrefabs.path;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        foreach (var e in entities)
        {
            DestroyVisualizationInstance(e);
            if (!e.hasPath) continue;

            var visualizedPathVertices = GetVisualizedPathVertices(e);
            var visualizationInstance  = CreateVisualizationInstance(e, _visualizationPrefab);

            SetupLineRendererPositions(visualizationInstance, visualizedPathVertices);
            OrderVisualizationHelper.PaintInTeamColor(visualizationInstance, e);
        }
    }

    private static Vector3[] GetVisualizedPathVertices(GameEntity e)
    {
        var count                   = e.path.waypoints.Count;
        var waypoints               = e.path.waypoints;
        var currentIndex            = e.path.currentIndex;
        
        var visualizedPathWaypoints = waypoints.Slice(currentIndex + 1, count - 1)
                                               .Select(v => v.ToVector3XZ())
                                               .ToList();
        
        AddFirstPointInterpolated(currentIndex, waypoints, visualizedPathWaypoints);
        AddLastPointInterpolated(waypoints, visualizedPathWaypoints);
        return visualizedPathWaypoints.ToArray();
    }

    private static void AddFirstPointInterpolated(int currentIndex, List<Vector2Int> pathWaypoints, List<Vector3> visualizedPathWaypoints)
    {
        if (currentIndex + 1 >= pathWaypoints.Count) return;
        
        var vector = Vector2.Lerp(pathWaypoints[currentIndex], pathWaypoints[currentIndex + 1], 0.5f);
        visualizedPathWaypoints.Insert(0, vector.ToVector3XZ());
    }

    private static void AddLastPointInterpolated(List<Vector2Int> pathWaypoints, List<Vector3> visualizedPathWaypoints)
    {
        if (pathWaypoints.Count < 2) return;
        
        var vector = Vector2.Lerp(pathWaypoints.GetFromEnd(1), pathWaypoints.GetFromEnd(2), 0.5f);
        visualizedPathWaypoints.Add(vector.ToVector3XZ());
    }

    private static void SetupLineRendererPositions(GameObject pathVisualizationInstance, Vector3[] visualizedPathVertices)
    {
        var lineRenderer = pathVisualizationInstance.GetComponent<LineRenderer>();
        lineRenderer.positionCount = visualizedPathVertices.Count();
        lineRenderer.SetPositions(visualizedPathVertices);
    }
}