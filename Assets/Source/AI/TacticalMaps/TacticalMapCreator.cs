using Entitas.Unity;
using ProceduralToolkit;
using UnityEngine;
using MathExtended.Matrices;

public static class TacticalMapCreator
{
    public static Matrix CreateTeamPlayersPositionMap(Contexts contexts, int teamID)
    {
        var mapSize     = contexts.game.mapSize.value;
        var tacticalMap = new Matrix(mapSize.x, mapSize.y);

        var players = contexts.game.GetEntitiesWithTeamID(teamID);
        
        foreach (var player in players)
        {
            var pos = player.gridPosition.value;
            
            tacticalMap[pos.x, pos.y]++;
        }

        return tacticalMap;
    }
    
    public static Matrix CreateAmountOfTeamPlayersThatCanBeSeenFromThisPositionMap(Contexts contexts, int teamID)
    {
        var mapSize     = contexts.game.mapSize.value;
        var tacticalMap = new Matrix(mapSize.x, mapSize.y);

        var players = contexts.game.GetEntitiesWithTeamID(teamID);

        for (int x = 0; x < tacticalMap.Rows; x++)
        {
            for (int y = 0; y < tacticalMap.Columns; y++)
            {
                foreach (var player in players)
                {
                    if (!player.hasUnityView) continue;

                    var raycastOrigin = new Vector3(x, 0, y);
                    raycastOrigin.y = 0.25f;

                    var targetPosition = player.worldPosition.value.ToVector3XZ();
                    targetPosition.y = 0.25f;

                    var raycastDirection = targetPosition - raycastOrigin;

                    //var maxDistance = Mathf.Min(Vector3.Distance(raycastOrigin, targetPosition), e.vision.distance);
                    var maxDistance = Vector3.Distance(raycastOrigin, targetPosition);

                    var raycastHits = Physics.RaycastAll(raycastOrigin, raycastDirection, maxDistance);

                    if (raycastHits.Length == 1)
                    {
                        var entity = (GameEntity)raycastHits[0].collider.gameObject.GetComponentInParent<EntityLink>()?.entity;

                        if (entity != null && entity.hasTeamID && entity.teamID.value == teamID)
                        {
                            tacticalMap[x, y]++;
                            Debug.DrawRay(raycastOrigin, raycastDirection, Color.magenta, 5f);
                        }
                    }
                }
            }
        }

        return tacticalMap;
    }

    public static Matrix CreateDistanceFromThisPositionToAllPositionsMap(Contexts contexts, Vector2Int from)
    {
        var mapSize     = contexts.game.mapSize.value;
        var tacticalMap = new Matrix(mapSize.x, mapSize.y);

        for (int x = 0; x < tacticalMap.Rows; x++)
        {
            for (int y = 0; y < tacticalMap.Columns; y++)
            {
                tacticalMap[x, y] = Vector2.Distance(from, new Vector2(x, y));
            }
        }

        return tacticalMap;
    }
}