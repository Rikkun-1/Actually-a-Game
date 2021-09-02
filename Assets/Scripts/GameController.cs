using System;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private double                    _timeAccumulated;
    [SerializeField] private double                    _tickDeltaTime  = 0.05;
    [SerializeField] private Vector2Int                _defaultMapSize = new Vector2Int(10, 10);
    private                  Contexts                  _contexts;
    private                  RootSystems               _systems;
    private                  EachFrameExecutionSystems _eachFrameExecutionSystems;
    private                  GameEntity                _timeHolder;
    
    private void Start()
    {
        _contexts                  = Contexts.sharedInstance;
        _systems                   = new RootSystems(_contexts);
        _eachFrameExecutionSystems = new EachFrameExecutionSystems(_contexts);

        _timeHolder = _contexts.game.CreateEntity();
        _timeHolder.AddGameTick(0, 0, 0);

        _contexts.game.SetMapSize(_defaultMapSize);

        _systems.Initialize();
        _eachFrameExecutionSystems.Initialize();
    }

    private void Update()
    {
        if (IsTimeForNewTick())
        {
            UpdateGameTick();
            _systems.Execute();
            _systems.Cleanup();
        }

        _eachFrameExecutionSystems.Execute();
        _eachFrameExecutionSystems.Cleanup();
    }

    private void OnDestroy()
    {
        _systems.TearDown();
        _eachFrameExecutionSystems.TearDown();
    }

    private bool IsTimeForNewTick()
    {
        _timeAccumulated += Time.deltaTime;

        return _timeAccumulated >= _tickDeltaTime;
    }

    private void UpdateGameTick()
    {
        var previous         = _timeHolder.gameTick;
        var newTimeFromStart = Math.Round(previous.timeFromStart + _tickDeltaTime, 3);

        _timeHolder.ReplaceGameTick(_tickDeltaTime,
                                    previous.tickFromStart + 1,
                                    newTimeFromStart);
        _timeAccumulated -= _tickDeltaTime;
    }
}