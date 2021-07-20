using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entitas;

public class CreatePlayerSystem : IInitializeSystem 
{    
    readonly Contexts _contexts;

    public CreatePlayerSystem(Contexts contexts)
   
    {        
        _contexts = contexts;
    }

    public void Initialize()
    {        
        var e = _contexts.game.CreateEntity();
        ;
    }
}