using System;
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

        var amount = 10;
        for (var i = 0; i < amount; i++)
        {
            var teamNumber = i % 2;

            var e = EntityCreator.CreateGameEntity();
            switch (Random.Range(0, 3))
            {
                case 0:
                    e.AddVision(teamNumber == 0 ? 0 : 180, 30, 500, 400);
                    e.AddViewPrefab(teamNumber == 0 ? "team 1 player shotgun" : "team 2 player shotgun");
                    e.AddTraversalSpeed(9);
                    WeaponProvider.GiveShotgun(e);
                    break;
                case 1:
                    e.AddVision(teamNumber == 0 ? 0 : 180, 30, 500, 200);
                    e.AddViewPrefab(teamNumber == 0 ? "team 1 player riffle" : "team 2 player riffle");
                    e.AddTraversalSpeed(7);
                    WeaponProvider.GiveRiffle(e);
                    break;
                case 2:
                    e.AddVision(teamNumber == 0 ? 0 : 180, 30, 500, 70);
                    e.AddViewPrefab(teamNumber == 0 ? "team 1 player sniper" : "team 2 player sniper");
                    e.AddTraversalSpeed(5);
                    WeaponProvider.GiveSniper(e);
                    break;
            }
            e.AddHealth(100);
            e.AddWorldPosition(new Vector2((i % amount / 2) * 6, teamNumber == 0 ? 1 : 28));
            //e.AddWorldPosition(new Vector2(Random.Range(0, _contexts.game.gridSize.value.x), Random.Range(0, _contexts.game.gridSize.value.y)));
            e.ReplaceTeamID(teamNumber);
            e.hasAI    = true;
            e.isPlayer = true;
        }

        var testWallsSystem = new TestGridWallsSystem(_contexts);
        for (var i = 0; i < 1000; i++)
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