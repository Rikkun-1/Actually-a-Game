﻿using System.Linq;
using Entitas;
using Source;
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

        if (Random.Range(0, 10) < 5)
        {
            var e = EntityCreator.CreateGameEntity();
            e.AddWorldPosition(position);
            e.AddHealth(50);
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