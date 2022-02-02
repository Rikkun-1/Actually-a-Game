using System.Linq;
using Entitas;
using ProceduralToolkit;
using UnityEngine;

public class TestGridWallsSystem : IExecuteSystem
{
    private readonly GameContext _game;

    public TestGridWallsSystem(Contexts contexts)
    {
        _game = contexts.game;
    }

    public void Execute()
    {
        var gridSize = _game.gridSize.value;

        var x = Random.Range(0, gridSize.x);
        var y = Random.Range(0, gridSize.y);

        var position           = new Vector2(x, y);
        var entitiesOnPosition =  _game.GetEntitiesWithGridPosition(position.ToVector2Int());

        if (Random.Range(0, 10) >= 5) return;
        
        var e = GameEntityCreator.CreateEntity();
        e.AddWorldPosition(position.ToVector3XZ());
        e.AddHealth(50, 50);
        e.ReplaceViewPrefab("Prefabs/Wall");
        switch (Random.Range(0, 4))
        {
            case 0
                when entitiesOnPosition.All(entity => !entity.hasWall || entity.wall.direction != Direction.Top):
                {
                    e.AddWall(Direction.Top);
                }
                break;
            case 1
                when entitiesOnPosition.All(entity => !entity.hasWall || entity.wall.direction != Direction.Bottom):
                {
                    e.AddWall(Direction.Bottom);
                }
                break;
            case 2
                when entitiesOnPosition.All(entity => !entity.hasWall || entity.wall.direction != Direction.Right):
                {
                    e.AddWall(Direction.Right);
                }
                break;
            case 3
                when entitiesOnPosition.All(entity => !entity.hasWall || entity.wall.direction != Direction.Left):
                {
                    e.AddWall(Direction.Left);
                }
                break;
            default:
                e.isDestroyed = true;
                break;
        }
    }
}