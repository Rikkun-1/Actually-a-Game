using System;
using UnityEngine;
using Random = UnityEngine.Random;

[Serializable]
public class SimulationController
{
    public float timeForOnePhaseCycle = 5f;
    public float timeUntilPhaseEnd;

    public  float                  tickDeltaTime = 0.02f;
    private Contexts               _contexts;
    public  SimulationPhaseSystems simulationPhaseSystems;

    public SimulationController(Contexts contexts)
    {
        _contexts              = contexts;
        simulationPhaseSystems = new SimulationPhaseSystems(_contexts);
    }

    public void Initialize(int wallsCount, int playersCount)
    {
        simulationPhaseSystems.Initialize();

        var amount = playersCount;
        for (var i = 0; i < amount; i++)
        {
            var teamNumber = i % 2;

            var e = EntityCreator.CreateGameEntity();
            switch (Random.Range(0, 3))
            {
                case 0:
                    e.AddVision(teamNumber == 0 ? 0 : 180, 30, 500, 400);
                    e.AddViewPrefab("SwatModel/SwatShotgun");
                    e.AddTraversalSpeed(1.8f);
                    WeaponProvider.GiveShotgun(e);
                    break;
                case 1:
                    e.AddVision(teamNumber == 0 ? 0 : 180, 30, 500, 200);
                    e.AddViewPrefab("SwatModel/SwatRifle");
                    e.AddTraversalSpeed(1.8f);
                    WeaponProvider.GiveRiffle(e);
                    break;
                case 2:
                    e.AddVision(teamNumber == 0 ? 0 : 180, 30, 500, 70);
                    e.AddViewPrefab("SwatModel/SwatSniper");
                    e.AddTraversalSpeed(1.8f);
                    WeaponProvider.GiveSniper(e);
                    break;
            }
            e.AddReactionDelay(2);
            e.AddHealth(250, 250);
            e.AddWorldPosition(new Vector3(10 + i % amount / 2 * 6, 0, teamNumber == 0 ? 1 : 28));
            e.ReplaceTeamID(teamNumber);
            e.hasAI                                    = true;
            e.isPlayer                                 = true;
            e.enableCalculateVelocityByPositionChanges = true;
        }

        var testWallsSystem = new TestGridWallsSystem(_contexts);
        for (var i = 0; i < wallsCount; i++)
        {
            testWallsSystem.Execute();
        }
    }

    public void UpdateSimulation()
    {
        if (timeUntilPhaseEnd >= tickDeltaTime)
        {
            timeUntilPhaseEnd -= tickDeltaTime;
            if (timeUntilPhaseEnd < tickDeltaTime) timeUntilPhaseEnd = 0;

            UpdateSimulationTickComponent();
            simulationPhaseSystems.Execute();
            simulationPhaseSystems.Cleanup();
        }
    }

    public void TearDown()
    {
        simulationPhaseSystems.TearDown();
    }


    private void UpdateSimulationTickComponent()
    {
        var previous         = _contexts.game.simulationTick;
        var newTimeFromStart = (float)Math.Round(previous.timeFromStart + tickDeltaTime, 3);

        _contexts.game.ReplaceSimulationTick(tickDeltaTime,
                                             previous.tickFromStart + 1,
                                             newTimeFromStart);
    }
}