using UnityEngine;

public static class GameEntityCreator
{
    public const string DefaultWallPrefabName   = "Prefabs/Wall";
    public const string DefaultWindowPrefabName = "Prefabs/Window";
    public const string DefaultCoverPrefabName  = "Prefabs/MidSizeCover";

    public static GameEntity CreateEntity(Contexts contexts)
    {
        var e = contexts.game.CreateEntity();
        e.AddId(e.creationIndex);

        return e;
    }

    public static GameEntity CreateEntity()
    {
        return CreateEntity(Contexts.sharedInstance);
    }

    public static GameEntity CreateEntity(Vector2Int worldPosition)
    {
        var e = CreateEntity(Contexts.sharedInstance);
        
        e.AddWorldPosition(worldPosition.ToVector3XZ());
        e.AddGridPosition(worldPosition);
        return e;
    }

    public static GameEntity CreateWall(Vector2Int worldPosition, Direction direction, string prefabName = DefaultWallPrefabName)
    {
        var e = CreateEntity(worldPosition);
        e.AddHealth(50, 50);
        e.AddViewPrefab(prefabName);
        e.AddWall(direction);

        return e;
    }

    public static GameEntity CreateWindow(Vector2Int worldPosition, Direction direction, string prefabName = DefaultWindowPrefabName)
    {
        var e = CreateWall(worldPosition, direction);
        e.ReplaceViewPrefab(prefabName);

        return e;
    }
    
    public static GameEntity CreateCover(Vector2Int worldPosition, string prefabName = DefaultCoverPrefabName)
    {
        var e = CreateEntity(worldPosition);
        e.AddHealth(50, 50);
        e.AddViewPrefab(prefabName);
        e.isNonWalkable = true;

        return e;
    }
}