using System.Collections.Generic;
using System.Linq;
using DesperateDevs.Utils;
using Entitas;
using GraphProcessor;
using UnityEngine;

public class ProcessAISystem : IInitializeSystem, IExecuteSystem
{
    private readonly IGroup<GameEntity>    _entities;
    private readonly GameContext           _game;
    private          BaseGraph             _baseGraph;
    private          ProcessGraphProcessor _processor;

    public ProcessAISystem(Contexts contexts)
    {
        _game     = contexts.game;
        _entities = contexts.game.GetGroup(GameMatcher.AllOf(GameMatcher.TeamID, GameMatcher.AI));
    }

    public void Initialize()
    {
        _baseGraph = _game.aIGraph.graph;
        _processor = new ProcessGraphProcessor(_baseGraph);
    }
    
    public void Execute()
    {
        foreach (var e in _entities)
        {
            _baseGraph.SetParameterValue("Entity ID", e.id.value);
            _processor.Run();
            
            var x     = _baseGraph.GetParameterValue<int>("SolutionX");
            var y     = _baseGraph.GetParameterValue<int>("SolutionY");
            var value = _baseGraph.GetParameterValue<int>("SolutionValue");
            
            var pos = new Vector2Int(x, y);
            e.ReplaceMoveToPositionOrder(pos);
        }
    }
}