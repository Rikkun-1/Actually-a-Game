using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entitas;

public class RemoveDamageSystem : ICleanupSystem
{
    private readonly Contexts           _contexts;
    private readonly IGroup<GameEntity> _entities;

    public RemoveDamageSystem(Contexts contexts)
    {
        _contexts = contexts;
        _entities = contexts.game.GetGroup(GameMatcher.Damage);
    }
    
    public void Cleanup()
    {
        foreach (var e in _entities.GetEntities())
        {
            e.RemoveDamage();
        }
    }
}