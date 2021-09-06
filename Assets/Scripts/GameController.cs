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
    
    private void Initialize()
    {
        var e = EntityCreator.createGameEntity();
        e.AddWorldPosition(new Vector2(1, 1));
        e.AddVision(new Vision(0, 30, 5, 100));
        e.AddViewPrefab("Cube");
    }
    
    private void Start()
    {
        _contexts                  = Contexts.sharedInstance;
        _systems                   = new RootSystems(_contexts);
        _eachFrameExecutionSystems = new EachFrameExecutionSystems(_contexts);

        _contexts.game.SetGameTick(0, 0, 0);
        _contexts.game.SetMapSize(_defaultMapSize);

        _systems.Initialize();
        _eachFrameExecutionSystems.Initialize();

        Initialize();
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
        var previous         = _contexts.game.gameTick;
        var newTimeFromStart = Math.Round(previous.timeFromStart + _tickDeltaTime, 3);

        _contexts.game.ReplaceGameTick(_tickDeltaTime,
                                       previous.tickFromStart + 1,
                                       newTimeFromStart);
        
        _timeAccumulated -= _tickDeltaTime;
    }
}