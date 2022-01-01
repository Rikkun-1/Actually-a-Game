﻿using System;
using Source;
using UnityEngine;
using Random = UnityEngine.Random;

[Serializable]
public class SimulationController
{
    public float timeForOnePhaseCycle = 2.5f;
    public float timeUntilPhaseEnd;

    public  float                  tickDeltaTime = 0.02f;
    public  float                  timeAccumulated;
    public  SimulationPhaseSystems simulationPhaseSystems;
    private Contexts               _contexts;

    public SimulationController(Contexts contexts)
    {
        _contexts              = contexts;
        simulationPhaseSystems = new SimulationPhaseSystems(_contexts);
    }

    public void Initialize()
    {
        simulationPhaseSystems.Initialize();

        var amount = 40;
        for (var i = 0; i < amount; i++)
        {
            var teamNumber = i % 2;

            var e = EntityCreator.CreateGameEntity();
            e.AddVision(teamNumber == 0 ? 0 : 180, 30, 500, 200);
            e.AddWeapon(15, 10, "bullet");
            e.AddHealth(100);
            e.AddTraversalSpeed(5);
            e.AddViewPrefab(teamNumber == 0 ? "team 1 player" : "team 2 player");
            //e.AddWorldPosition(new Vector2(i * 3, teamNumber == 0 ? 1 : 19));
            e.AddWorldPosition(new Vector2(Random.Range(0, _contexts.game.gridSize.value.x), Random.Range(0, _contexts.game.gridSize.value.y)));
            e.ReplaceTeamID(teamNumber);
            e.hasAI    = true;
            e.isPlayer = true;
        }

        var testWallsSystem = new TestGridWallsSystem(_contexts);
        for (var i = 0; i < 3000; i++)
        {
            testWallsSystem.Execute();
        }
    }

    public bool UpdateSimulation(float dt)
    {
        if (timeUntilPhaseEnd > tickDeltaTime * 0.9 && IsTimeForNewTick(dt))
        {
            timeUntilPhaseEnd = timeUntilPhaseEnd < tickDeltaTime
                                    ? 0
                                    : timeUntilPhaseEnd - tickDeltaTime;
            UpdateSimulationTickComponent();
            simulationPhaseSystems.Execute();
            simulationPhaseSystems.Cleanup();

            return true;
        }

        return false;
    }

    public void TearDown()
    {
        simulationPhaseSystems.TearDown();
    }

    private bool IsTimeForNewTick(float dt)
    {
        timeAccumulated += dt;

        return timeAccumulated >= tickDeltaTime;
    }

    private void UpdateSimulationTickComponent()
    {
        var previous         = _contexts.game.simulationTick;
        var newTimeFromStart = (float)Math.Round(previous.timeFromStart + tickDeltaTime, 3);

        _contexts.game.ReplaceSimulationTick(tickDeltaTime,
                                             previous.tickFromStart + 1,
                                             newTimeFromStart);

        timeAccumulated -= tickDeltaTime;
    }
}