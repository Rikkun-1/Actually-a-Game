using Shouldly;
using UnityEngine;

public class describe_grid_systems : entitas_tests
{
    private void describe_delete_entities_outside_map_system()
    {
        var        mapSize = new Vector2Int(10, 10);
        GameEntity e       = null;
        
        before = () =>
        {
            Setup();
            systems.Add(new DeleteEntitiesOutsideMapSystem(contexts));
            contexts.game.SetMapSize(mapSize);
            e = CreateEntity();
        };

        it["destroys entities outside map"] = () =>
        {
            var gridPosition = new Vector2Int(mapSize.x, mapSize.y);
            e.AddGridPosition(gridPosition);
            systems.Execute();
            e.isDestroyed.ShouldBeTrue();
        };
        
        it["does not destroy entities inside map"] = () =>
        {
            for (int x = 0; x < mapSize.x; x++)
            {
                for (int y = 0; y < mapSize.y; y++)
                {
                    var gridPosition = new Vector2Int(x, y);
                    e.ReplaceGridPosition(gridPosition);
                    systems.Execute();
                    e.isDestroyed.ShouldBeFalse();
                }
            }
        };
    }
}
