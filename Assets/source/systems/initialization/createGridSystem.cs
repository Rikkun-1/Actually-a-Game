using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entitas;

public class CreateGridSystem : IInitializeSystem
{
    Vector2Int gridSize = new Vector2Int(10, 10);

    readonly Contexts _contexts;

    public CreateGridSystem(Contexts contexts)
    {
        _contexts = contexts;
    }
    public void Initialize()
    {
        for (int i = 0; i < gridSize.x; i++)
        {
            for (int j = 0; j < gridSize.x; j++)
            {
                var e = _contexts.game.CreateEntity();
                e.AddPosition(new Vector2Int(i, j));
            }
        }
    }
}