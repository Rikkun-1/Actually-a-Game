using System.Collections.Generic;
using System.Linq;
using Roy_T.AStar.Primitives;
using UnityEngine;

namespace Roy_T.AStar.Paths
{
    public static class PathExtensions
    {
        public static List<Vector2Int> GetWaypointsFromPath(this Path path)
        {
            var waypoints = new List<Vector2Int>(path.Edges.Count + 1);

            if (path.Edges.Count > 0)
            {
                waypoints.Add(path.Edges[0].Start.Position.ToVector2Int());
                waypoints.AddRange(path.Edges
                                       .Select(edge => edge.End.Position.ToVector2Int())
                                       .ToList());
            }

            return waypoints;
        }
    }
}