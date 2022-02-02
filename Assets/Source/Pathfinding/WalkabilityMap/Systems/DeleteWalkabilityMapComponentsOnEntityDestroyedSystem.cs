﻿using Entitas;

public class DeleteWalkabilityMapComponentsOnEntityDestroyedSystem : IExecuteSystem
{
    private readonly IGroup<GameEntity> _entities;

    public DeleteWalkabilityMapComponentsOnEntityDestroyedSystem(Contexts contexts)
    {
        _entities = contexts.game.GetGroup(GameMatcher.AllOf(GameMatcher.Destroyed)
                                                      .AnyOf(GameMatcher.NonWalkable,
                                                             GameMatcher.Wall)
                                                      .NoneOf(GameMatcher.Indestructible));
    }

    public void Execute()
    {
        foreach (var e in _entities.GetEntities())
        {
            e.isNonWalkable = false;
            e.RemoveWall();
        }
    }
}