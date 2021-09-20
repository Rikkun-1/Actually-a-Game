using System;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private float                     _timeAccumulated;
    [SerializeField] private float                     _tickDeltaTime  = 0.05f;
    [SerializeField] private Vector2Int                _defaultMapSize = new Vector2Int(10, 10);
    private                  Contexts                  _contexts;
    private                  EachFrameExecutionSystems _eachFrameExecutionSystems;
    private                  RootSystems               _systems;

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

    private void Initialize()
    {
        // for (int i = 0; i < 10; i++)
        // {
        //     var e = EntityCreator.CreateGameEntity();
        //     e.AddWorldPosition(new Vector2(i, 1));
        //     e.AddVision(new Vision(0, 30, 5, 100));
        //     e.AddViewPrefab("Cube");
        //     e.AddPathRequest(e.worldPosition.value.ToVector2Int(), new Vector2Int(8, 8));
        //     e.AddShootAtPositionOrder(new Vector2(8, i));
        //     e.AddTraversalSpeed(5);
        // }
        var e = EntityCreator.CreateGameEntity();
        e.AddVision(0, 30, 500, 100);
        e.AddShootAtEntityOrder(1);
        e.AddWeapon(15, 10, "bullet");
        e.AddViewPrefab("Cube");
        e.AddTraversalSpeed(5);
        e.AddWorldPosition(new Vector2(1, 1));
        e.AddPathRequest(e.worldPosition.value.ToVector2Int(), e.worldPosition.value.ToVector2Int());
        e.AddHealth(1000);

        e = EntityCreator.CreateGameEntity();
        e.AddVision(0, 30, 500, 1000);
        e.AddShootAtEntityOrder(0);
        e.AddWeapon(15, 10, "bullet");
        e.AddViewPrefab("Cube");
        e.AddTraversalSpeed(5);
        e.AddWorldPosition(new Vector2(19, 19));
        e.AddPathRequest(e.worldPosition.value.ToVector2Int(), e.worldPosition.value.ToVector2Int());
        e.AddHealth(100);

        var testWallsSystem = new TestGridWallsSystem(_contexts);
        for (var i = 0; i < 200; i++)
        {
            testWallsSystem.Execute();
        }
    }

    private bool IsTimeForNewTick()
    {
        _timeAccumulated += Time.deltaTime;

        return _timeAccumulated >= _tickDeltaTime;
    }

    private void UpdateGameTick()
    {
        var previous         = _contexts.game.gameTick;
        var newTimeFromStart = (float)Math.Round(previous.timeFromStart + _tickDeltaTime, 3);

        _contexts.game.ReplaceGameTick(_tickDeltaTime,
                                       previous.tickFromStart + 1,
                                       newTimeFromStart);

        _timeAccumulated -= _tickDeltaTime;
    }
}