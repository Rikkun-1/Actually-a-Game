using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entitas;

using System;

public class TestSystem : IExecuteSystem
{
    readonly IGroup<GameEntity> entities;

    public TestSystem(Contexts contexts)
    {
        this.entities = contexts.game.GetGroup(GameMatcher.Position);
    }

    public void Execute()
    {
        var rand = UnityEngine.Random.Range(0, this.entities.count);
        var e = this.entities.GetEntities()[rand];

        e.isNonWalkable = UnityEngine.Random.Range(0, 2) == 0;
        e.ReplaceViewPrefab(e.isNonWalkable ? "nonWalkable" : "floor");
    }
}