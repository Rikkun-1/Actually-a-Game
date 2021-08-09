using System.Linq;
using UnityEngine;
using Entitas;

public class TestGridWallsSystem: IExecuteSystem
{
    readonly GameContext game;

    public TestGridWallsSystem(Contexts contexts)
    {
        this.game = contexts.game;
    }

    public void Execute()
    {
        var mapSize = game.GetEntities(GameMatcher.MapSize).ToList().SingleEntity().mapSize.value;

        var x = UnityEngine.Random.Range(0, mapSize.x);
        var y = UnityEngine.Random.Range(0, mapSize.y);

        if (UnityEngine.Random.Range(0, 10) < 5)
        {
            switch (UnityEngine.Random.Range(0, 4))
            {
                case 0:
                    if (game.GetEntitiesWithPosition(new Vector2Int(x, y)).All(e => e.isNorthWall == false))
                    {
                        var e = game.CreateEntity();
                        e.AddPosition(new Vector2Int(x, y));
                        e.isNorthWall = true;
                        e.ReplaceViewPrefab("NorthWall");
                    }
                    break;
                case 1:
                    if (game.GetEntitiesWithPosition(new Vector2Int(x, y)).All(e => e.isSouthWall == false))
                    {
                        var e = game.CreateEntity();
                        e.AddPosition(new Vector2Int(x, y));
                        e.isSouthWall = true;
                        e.ReplaceViewPrefab("SouthWall");
                    }
                    break;
                case 2:
                    if (game.GetEntitiesWithPosition(new Vector2Int(x, y)).All(e => e.isEastWall == false))
                    {
                        var e = game.CreateEntity();
                        e.AddPosition(new Vector2Int(x, y));
                        e.isEastWall = true;
                        e.ReplaceViewPrefab("EastWall");
                    }
                    break;
                case 3:
                    if (game.GetEntitiesWithPosition(new Vector2Int(x, y)).All(e => e.isWestWall == false))
                    {
                        var e = game.CreateEntity();
                        e.AddPosition(new Vector2Int(x, y));
                        e.isWestWall = true;
                        e.ReplaceViewPrefab("WestWall");
                    }
                    break;
            }
        }
    }
}