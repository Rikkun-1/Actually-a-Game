using System.Linq;
using ProceduralToolkit;
using UnityEngine;

public static class RandomMapGenerator
{
    public static void PlaceRandomWalls(GameContext game, int amount, string prefabName = GameEntityCreator.DefaultWallPrefabName)
    {
        bool checkWall(Vector2Int gridPosition, Direction direction)
        {
            return  game.GetEntitiesWithGridPosition(gridPosition).Any(entity => entity.hasWall && entity.wall.direction == direction);
        }
        
        var gridSize = game.gridSize.value;

        for (var i = 0; i < amount;)
        {
            var gridPosition    = RandomE.Range(Vector2Int.zero, gridSize);
            var direction = RandomExtensions.RandomLateralDirection();

            if(checkWall(gridPosition, direction)) continue;

            GameEntityCreator.CreateWall(gridPosition, direction, prefabName);
            i++;
        }
    }
    
    public static void PlaceRandomWindows(GameContext game, int amount)
    {
        PlaceRandomWalls(game, amount, GameEntityCreator.DefaultWindowPrefabName);
    }

    public static void PlaceRandomCovers(GameContext game, int amount)
    {
        bool isTaken(Vector2Int gridPosition)
        {
            return game.GetEntitiesWithGridPosition(gridPosition).Any(e => e.isPlayer || e.isNonWalkable);
        }
        
        var gridSize = game.gridSize.value;
        
        for (var i = 0; i < amount;)
        {
            var gridPosition = RandomE.Range(Vector2Int.zero, gridSize);

            if (isTaken(gridPosition)) continue;

            GameEntityCreator.CreateCover(gridPosition);
            i++;
        }
    }
}
