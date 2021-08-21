using System;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private RootSystems _systems;
    private Contexts    _contexts;
    private GameEntity  _timeHolder;
    
    [SerializeField] private double      _timeAccumulated;
    [SerializeField] private double      _tickDeltaTime = 0.05;

    private void Start()
    {
        _contexts = Contexts.sharedInstance;
        
        _timeHolder = _contexts.game.CreateEntity();
        _timeHolder.AddGameTick(0, 0, 0);
        
        _systems  = new RootSystems(_contexts);
        _systems.Initialize();
    }

    private void Update()
    {
        UpdateGameTick();

        _systems.Execute();
        _systems.Cleanup();
    }

    private void UpdateGameTick()
    {
        _timeAccumulated += Time.deltaTime;

        if (_timeAccumulated >= _tickDeltaTime)
        {
            var previous         = _timeHolder.gameTick;
            var newTimeFromStart = Math.Round(previous.TimeFromStart + _tickDeltaTime, 2);

            _timeHolder.ReplaceGameTick(newTimeFromStart,
                                        _tickDeltaTime,
                                        previous.TickFromStart + 1);
            _timeAccumulated -= _tickDeltaTime;
        }
    }

    private void OnDestroy()
    {
        _systems.TearDown();
    }
}