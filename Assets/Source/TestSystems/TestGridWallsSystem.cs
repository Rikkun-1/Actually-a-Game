using System.Linq;
using Entitas;
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
        var mapSize = _game.mapSize.value;

        var x = Random.Range(0, mapSize.x);
        var y = Random.Range(0, mapSize.y);

        var position           = new Vector2Int(x, y);
        var entitiesOnPosition =  _game.GetEntitiesWithGridPosition(position);

        if (Random.Range(0, 10) < 5)
        {
            var e = _game.CreateEntity();
            e.AddGridPosition(position);
            switch (Random.Range(0, 4))
            {
                case 0
                    when entitiesOnPosition.All(entity => entity.isNorthWall == false):
                    {
                        e.isNorthWall = true;
                        e.ReplaceViewPrefab("NorthWall");
                    }
                    break;
                case 1
                    when entitiesOnPosition.All(entity => entity.isSouthWall == false):
                    {
                        e.isSouthWall = true;
                        e.ReplaceViewPrefab("SouthWall");
                    }
                    break;
                case 2
                    when entitiesOnPosition.All(entity => entity.isEastWall == false):
                    {
                        e.isEastWall = true;
                        e.ReplaceViewPrefab("EastWall");
                    }
                    break;
                case 3
                    when entitiesOnPosition.All(entity => entity.isWestWall == false):
                    {
                        e.isWestWall = true;
                        e.ReplaceViewPrefab("WestWall");
                    }
                    break;
                default:
                    e.isDestroyed = true;
                    break;
            }
        }
    }
}