using Shouldly;
using UnityEngine;

public class describe_walkability_map_systems : entitas_tests
{
    private void describe_resize_pathfinding_map_system()
    {
        GameEntity e       = null;
        
        before = () =>
        {
            Setup();
            systems.Add(new ResizePathfindingMapSystem(contexts));
            e = CreateEntity();
        };

        it["creates new pathfinding grid if map size is changed"] = () =>
        {        
            var mapSize = new Vector2Int(10, 10);
            contexts.game.SetMapSize(mapSize);
            systems.Execute();
            var pathfindingGrid = contexts.game.pathfindingGrid.value;
            pathfindingGrid.Columns.ShouldBe(mapSize.x);
            pathfindingGrid.Rows.ShouldBe(mapSize.y);
        };

        it["does not change pathfinding grid if map size is not changed"] = () =>
        {            
            systems.Execute();
            contexts.game.hasPathfindingGrid.ShouldBeFalse();
        };
    }

    private void describe_delete_walkability_map_components_on_entity_destroyed_system()
    {
        GameEntity e = null;
        
        before = () =>
        {
            Setup();
            systems.Add(new DeleteWalkabilityMapComponentsOnEntityDestroyedSystem(contexts));
            e               = CreateEntity();
            e.isNonWalkable = true;
            e.isNorthWall   = true;
            e.isSouthWall   = true;
            e.isWestWall    = true;
            e.isEastWall    = true;
        };

        it["deletes walkability map components if entity destroyed"] = () =>
        {
            e.isDestroyed = true;
            systems.Execute();
            e.isNonWalkable.ShouldBeFalse();
            e.isNorthWall.ShouldBeFalse();
            e.isSouthWall.ShouldBeFalse();
            e.isWestWall.ShouldBeFalse();
            e.isEastWall.ShouldBeFalse();
        };
        
        it["does not delete walkability map components if entity is not destroyed"] = () =>
        {
            e.isDestroyed = false;
            systems.Execute();
            e.isNonWalkable.ShouldBeTrue();
            e.isNorthWall.ShouldBeTrue();
            e.isSouthWall.ShouldBeTrue();
            e.isWestWall.ShouldBeTrue();
            e.isEastWall.ShouldBeTrue();
        };
    }
}
