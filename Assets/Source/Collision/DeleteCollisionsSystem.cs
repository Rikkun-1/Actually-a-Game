using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entitas;

public class DeleteCollisionsSystem : ICleanupSystem
{
    private readonly Contexts           _contexts;
    private readonly IGroup<GameEntity> _entities;

    public DeleteCollisionsSystem(Contexts contexts)
    {
        _contexts = contexts;
        _entities = contexts.game.GetGroup(GameMatcher.Collision);
    }
    
    public void Cleanup()
    {
        foreach (var e in _entities)
        {
            e.isDestroyed = true;
        }
    }
}