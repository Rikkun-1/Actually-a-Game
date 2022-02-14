using UnityEngine;
using Random = UnityEngine.Random;

public static class MapCreator
{
    public static void GenerateMap(GameContext game, int wallsCount, int windowCount, int coversCount, int playersCount, int spacing)
    {
        
        PopulateMapWithFloor(game.gridSize.value);
        CreateTeam(playersCount, 0, spacing, 1);
        CreateTeam(playersCount, 0, spacing, 20);
        // CreateTeam(playersCount, 0, spacing, 50);
        // CreateTeam(playersCount, 0, spacing, 60);
        // CreateTeam(playersCount, 0, spacing, 90);

        CreateTeam(playersCount, 1, spacing, 10);
        CreateTeam(playersCount, 1, spacing, 30);
        // CreateTeam(playersCount, 1, spacing, 70);
        // CreateTeam(playersCount, 1, spacing, 80);
        // CreateTeam(playersCount, 1, spacing, 100);

        RandomMapGenerator.PlaceRandomWalls(game, wallsCount);
        RandomMapGenerator.PlaceRandomWindows(game, windowCount);
        RandomMapGenerator.PlaceRandomCovers(game, coversCount);
    }

    private static void PopulateMapWithFloor(Vector2Int size)
    {
        for (var x = 0; x < size.x; x++)
        {
            for (var z = 0; z < size.y; z++)
            {
                var e = GameEntityCreator.CreateEntity();
                e.AddWorldPosition(new Vector3(x, 0, z));
                e.AddViewPrefab("Prefabs/floor");
            }
        }
    }

    private static void CreateTeam(int playersCount, int teamNumber, int spacing, int spawnZ)
    {
        for (var i = 0; i < playersCount; i++)
        {
            var position = new Vector2Int(5 + i * spacing, spawnZ);
            CreateRandomUnit(teamNumber, position);
        }
    }

    private static GameEntity CreateRandomUnit(int teamNumber, Vector2Int position)
    {
        var e = Random.Range(0, 3) switch
        {
            0 => UnitCreator.CreateUnit(position, UnitClass.Shotgun),
            1 => UnitCreator.CreateUnit(position, UnitClass.Rifle),
            2 => UnitCreator.CreateUnit(position, UnitClass.Sniper)
        };
        
        e.vision.directionAngle = teamNumber == 0 ? 0 : 180;
        e.UpdateVision();
        
        e.ReplaceTeamID(teamNumber);
        e.AddTeamColor(teamNumber == 1 ? Color.red : Color.green);
        
        //e.hasAI         = teamNumber == 1;
        e.hasAI         = true;
        e.isInteractive = teamNumber != 1;
        return e;
    }
}