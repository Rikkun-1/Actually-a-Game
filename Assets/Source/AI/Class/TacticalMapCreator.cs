using System.Linq;
using Unity.Collections;
using UnityEngine;

public static class TacticalMapCreator
{
    private static Matrix CreateMatrix(Vector2Int size)
    {
        return new Matrix(size.x, size.y);
    }

    public static Matrix AmountOfTeamPlayersThatCanBeSeenFromThisPosition(GameContext game, int teamID)
    {
        return CacheLambdaResult.CacheResult(() =>
        {
            void setupRaycastBatch(int width, int height, int depth, GameEntity[] players, Vector3[] origins, NativeArray<RaycastCommand> commands, int layerMask)
            {
                for (var x = 0; x < width; x++)
                {
                    for (var y = 0; y < height; y++)
                    {
                        for (var z = 0; z < depth; z++)
                        {
                            var raycastOrigin = Vector3.zero;
    
                            raycastOrigin.x = x;
                            raycastOrigin.y = 1.4f;
                            raycastOrigin.z = y;
    
                            var targetPosition = players[z].worldPosition.value;
                            var direction      = targetPosition - raycastOrigin;
                            var distance       = direction.magnitude;
    
                            var index = z *  width * height + y * width + x;
                            origins[index]  = raycastOrigin;
                            commands[index] = new RaycastCommand(raycastOrigin, direction, distance, layerMask);
                        }
                    }
                }
            }
            
            void processResults(int amountOfRaycasts1, NativeArray<RaycastHit> raycastHits, Vector3[] origins, Matrix matrix)
            {
                for (var i = 0; i < amountOfRaycasts1; i++)
                {
                    if (raycastHits[i].collider != null) continue;
                    var origin = origins[i];
                    var x      = (int)origin.x;
                    var y      = (int)origin.z;
                    matrix[x, y]++;
                }
            }
        
            var size        = game.gridSize.value;
            var tacticalMap = CreateMatrix(size);

            var players = game.GetEntitiesWithTeamID(teamID).Where(e => !e.isDestroyed).ToArray();

            var width  = size.x;
            var height = size.y;
            var depth  = players.Length;
            
            var amountOfRaycasts = width * height * depth;

            var results  = new NativeArray<RaycastHit>(amountOfRaycasts, Allocator.TempJob);
            var commands = new NativeArray<RaycastCommand>(amountOfRaycasts, Allocator.TempJob);
            var origins  = new Vector3[amountOfRaycasts];

            var layerMask = LayerMask.GetMask("Default");

            setupRaycastBatch(width, height, depth, players, origins, commands, layerMask);

            RaycastCommand.ScheduleBatch(commands, results, amountOfRaycasts / 64)
                          .Complete();
            
            processResults(amountOfRaycasts, results, origins, tacticalMap);

            results.Dispose();
            commands.Dispose();

            return tacticalMap;
        });
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