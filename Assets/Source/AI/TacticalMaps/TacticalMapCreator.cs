using Entitas.Unity;
using ProceduralToolkit;
using UnityEngine;

public static class TacticalMapCreator
{
    public static TacticalMap CreateTeamPlayersPositionMap(GameContext game, int teamID)
    {
        var gridSize    = game.gridSize.value;
        var tacticalMap = new TacticalMap(gridSize.x, gridSize.y);

        var players = game.GetEntitiesWithTeamID(teamID);

        foreach (var player in players)
        {
            var pos = player.gridPosition.value;

            tacticalMap[pos.x, pos.y]++;
        }

        return tacticalMap;
    }

    public static TacticalMap CreateAmountOfTeamPlayersThatCanBeSeenFromThisPositionMap(GameContext game, int teamID)
    {
        var gridSize    = game.gridSize.value;
        var tacticalMap = new TacticalMap(gridSize.x, gridSize.y);

        var players = game.GetEntitiesWithTeamID(teamID);

        for (var x = 0; x < tacticalMap.width; x++)
        {
            for (var y = 0; y < tacticalMap.height; y++)
            {
                foreach (var player in players)
                {
                    if (!player.hasUnityView) continue;

                    var raycastOrigin = new Vector3(x, 0.25f, y);

                    var targetPosition = player.worldPosition.value.ToVector3XZ();
                    targetPosition.y = 0.25f;

                    var raycastDirection = targetPosition - raycastOrigin;

                    //var maxDistance = Mathf.Min(Vector3.Distance(raycastOrigin, targetPosition), e.vision.distance);
                    var maxDistance = Vector3.Distance(raycastOrigin, targetPosition);

                    var raycastHits = Physics.RaycastAll(raycastOrigin, raycastDirection, maxDistance);

                    var clearVision = false;
                    foreach (var hit in raycastHits)
                    {
                        var entity = (GameEntity)hit.collider.gameObject.GetComponentInParent<EntityLink>()?.entity;

                        if (entity == null || !entity.isPlayer)
                        {
                            clearVision = false;
                            break;
                        }

                        if (player.id == entity.id)
                        {
                            clearVision = true;
                        }
                    }

                    if (clearVision)
                    {
                        tacticalMap[x, y]++;
                        Debug.DrawRay(raycastOrigin, raycastDirection, Color.magenta, 5f);
                    }
                }
            }
        }

        return tacticalMap;
    }

    public static TacticalMap CreateDistanceFromThisPositionToAllPositionsMap(GameContext game, Vector2Int from)
    {
        var gridSize    = game.gridSize.value;
        var tacticalMap = new TacticalMap(gridSize.x, gridSize.y);

        for (var x = 0; x < tacticalMap.width; x++)
        {
            for (var y = 0; y < tacticalMap.height; y++)
            {
                var distance = Vector2.Distance(from, new Vector2(x, y));
                tacticalMap[x, y] = Mathf.RoundToInt(distance);
            }
        }

        return tacticalMap;
    }
}