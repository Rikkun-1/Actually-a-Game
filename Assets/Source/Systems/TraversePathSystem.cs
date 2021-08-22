﻿using System.Collections.Generic;
using System.Linq;
using Entitas;
using UnityEngine;

public class TraversePathSystem : ReactiveSystem<GameEntity>
{
    private IGroup<GameEntity> _entities;
    private Contexts           _contexts;

    public TraversePathSystem(Contexts contexts) : base(contexts.game)
    {
        _contexts = contexts;
        _entities = contexts.game.GetGroup(GameMatcher.AllOf(GameMatcher.Position, GameMatcher.Path));
        
        //var e = _contexts.game.CreateEntity();
        //e.isIndestructible = true;
        //e.AddPosition(new Vector2Int(1, 1));
        //e.AddViewPrefab("Cube");
        //e.AddPathRequest(e.position.Value, new Vector2Int(7, 7));   
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        => context.CreateCollector(GameMatcher.GameTick.Added());

    protected override bool Filter(GameEntity entity) => true;

    protected override void Execute(List<GameEntity> entities)
    {
        foreach (var e in _entities.GetEntities())
        {
            if (e.path.CurrentIndex < e.path.Path.Edges.Count)
            {
                var nextPosition = e.path.Path.Edges[e.path.CurrentIndex].End.Position;
                var newPosition  = new Vector2Int((int)nextPosition.X, (int)nextPosition.Y);

                e.ReplacePosition(newPosition);

                e.ReplacePath(e.path.Path, e.path.CurrentIndex + 1);
            }
            else
            {
                var mapSize = _contexts.game.GetEntities(GameMatcher.MapSize).ToList().SingleEntity().mapSize.Value;

                var x   = Random.Range(0, mapSize.x);
                var y   = Random.Range(0, mapSize.y);
                
                var end = new Vector2Int(x, y);
                e.ReplacePathRequest(e.position.Value, end);
                e.RemovePath();
            }
        }
    }
}