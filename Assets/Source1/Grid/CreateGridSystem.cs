using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entitas;
using Entitas.Unity;

public class CreateGridSystem : IInitializeSystem
{
    Vector2Int gridSize = new Vector2Int(5, 5);

    readonly Contexts contexts;

    public CreateGridSystem(Contexts contexts)
    {
        this.contexts = contexts;
    }

    public void Initialize()
    {
        for (int i = 0; i < gridSize.x; i++)
        {
            for (int j = 0; j < gridSize.y; j++)
            {
                var e = contexts.game.CreateEntity();
                e.AddPosition(new Vector2Int(i, j));
                e.AddViewPrefab("cell");
            }
        }
    }
}