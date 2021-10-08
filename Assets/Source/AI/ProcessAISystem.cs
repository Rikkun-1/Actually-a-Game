using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entitas;

public class ProcessAISystem : IExecuteSystem
{
    private readonly IGroup<GameEntity> _entities;

    public ProcessAISystem(Contexts contexts)
    {
        _entities = contexts.game.GetGroup(GameMatcher.AI);
    }

    public void Execute()
    {
        
    }
}