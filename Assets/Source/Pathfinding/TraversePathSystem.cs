﻿using System.Collections.Generic;
using System.Linq;
using Entitas;
using UnityEngine;

public class TraversePathSystem : IExecuteSystem
{
    private readonly Contexts           _contexts;
    private readonly IGroup<GameEntity> _entities;

    public TraversePathSystem(Contexts contexts)
    {
        _contexts = contexts;
        _entities = contexts.game.GetGroup(GameMatcher.AllOf(GameMatcher.Position, 
                                                             GameMatcher.Path));

        var e = _contexts.game.CreateEntity();
        e.isIndestructible = true;
        e.AddPosition(new Vector2Int(1, 1));
        e.AddViewPrefab("Cube");
        e.AddPathRequest(e.position.value, new Vector2Int(7, 7));   
    }

    public void Execute()
    {
        foreach (var e in _entities.GetEntities())
        {
            if (e.path.currentIndex < e.path.path.Edges.Count)
            {
                var nextPosition = e.path.path.Edges[e.path.currentIndex].End.Position;
                var newPosition  = new Vector2Int((int)nextPosition.X, (int)nextPosition.Y);

                e.ReplacePosition(newPosition);
                e.ReplacePath(e.path.path, e.path.currentIndex + 1);
            }
            else
            {
                var mapSize = _contexts.game.GetEntities(GameMatcher.MapSize).ToList()
                                       .SingleEntity().mapSize.value;

                var x = Random.Range(0, mapSize.x);
                var y = Random.Range(0, mapSize.y);

                var end = new Vector2Int(x, y);
                e.ReplacePathRequest(e.position.value, end);
                e.RemovePath();
            }
        }
    }
}