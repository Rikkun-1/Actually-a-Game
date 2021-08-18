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
        var mapSize = _game.GetEntities(GameMatcher.MapSize).ToList().SingleEntity().mapSize.Value;

        var x = Random.Range(0, mapSize.x);
        var y = Random.Range(0, mapSize.y);

        if (Random.Range(0, 10) < 5)
        {
            switch (Random.Range(0, 4))
            {
                case 0:
                    if (_game.GetEntitiesWithPosition(new Vector2Int(x, y)).All(e => e.isNorthWall == false))
                    {
                        var e = _game.CreateEntity();
                        e.AddPosition(new Vector2Int(x, y));
                        e.isNorthWall = true;
                        e.ReplaceViewPrefab("NorthWall");
                    }

                    break;

                case 1:
                    if (_game.GetEntitiesWithPosition(new Vector2Int(x, y)).All(e => e.isSouthWall == false))
                    {
                        var e = _game.CreateEntity();
                        e.AddPosition(new Vector2Int(x, y));
                        e.isSouthWall = true;
                        e.ReplaceViewPrefab("SouthWall");
                    }

                    break;

                case 2:
                    if (_game.GetEntitiesWithPosition(new Vector2Int(x, y)).All(e => e.isEastWall == false))
                    {
                        var e = _game.CreateEntity();
                        e.AddPosition(new Vector2Int(x, y));
                        e.isEastWall = true;
                        e.ReplaceViewPrefab("EastWall");
                    }

                    break;

                case 3:
                    if (_game.GetEntitiesWithPosition(new Vector2Int(x, y)).All(e => e.isWestWall == false))
                    {
                        var e = _game.CreateEntity();
                        e.AddPosition(new Vector2Int(x, y));
                        e.isWestWall = true;
                        e.ReplaceViewPrefab("WestWall");
                    }

                    break;
            }
        }
    }
}