using System.Linq;
using UnityEngine;

public static class TacticalMapCreator
{
    private static Matrix CreateMatrix(Vector2Int size)
    {
        return new Matrix(size.x, size.y);
    }

    public static Matrix AmountOfTeamPlayersThatCanBeSeenFromThisPosition(GameContext game, int teamID)
    {
        var players = game.GetEntitiesWithTeamID(teamID).Where(e => !e.isDead);

        var tacticalMap = CreateMatrix(game.gridSize.value);

        // tacticalMap.Loop((x, y) =>
        // {
        for (var x = 0; x < tacticalMap.width; x++)
        {
            for (var y = 0; y < tacticalMap.height; y++)
            {
                foreach (var player in players)
                {
                    var raycastOrigin = new Vector3(x, 0.4f, y);

                    if (RaycastHelper.IsInClearVision(raycastOrigin, player))
                    {
                        tacticalMap[x, y]++;
                    }
                }
            }
        }

        return tacticalMap;
    }

    public static Matrix AmountOfEnemiesThatCanBeSeenFromThisPosition(GameContext game, int entityTeamID)
    {
        var result       = CreateMatrix(game.gridSize.value);
        var enemyTeamIDs = TeamIDHelper.GetEnemyTeamIDs(game, entityTeamID);

        foreach (var enemyTeamID in enemyTeamIDs)
        {
            result += AmountOfTeamPlayersThatCanBeSeenFromThisPosition(game, enemyTeamID);
        }

        return result;
    }

    public static Matrix DistanceFromThisToAllPositions(GameContext game, Vector2Int from)
    {
        var result = CreateMatrix(game.gridSize.value)
           .ForEach((x, y, value)
                        => Mathf.RoundToInt(Vector2.Distance(from, new Vector2(x, y))));

        return result;
    }
}