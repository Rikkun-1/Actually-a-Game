using System;
using UnityEngine;
using Random = UnityEngine.Random;

[Serializable]
public class SimulationController
{
    public float timeForOnePhaseCycle = 5f;
    public float timeUntilPhaseEnd;
    public float tickDeltaTime = 0.02f;

    public event Action OnSimulationPhaseEnd;

    public SimulationPhaseSystems simulationPhaseSystems;

    private Contexts _contexts;

    public SimulationController(Contexts contexts)
    {
        _contexts              = contexts;
        simulationPhaseSystems = new SimulationPhaseSystems(_contexts);
    }

    public void Initialize(int wallsCount, int playersCount)
    {
        var bulletHitEffectPrefab = Resources.Load<ParticleSystem>("Effects/Weapon Effects/Prefabs/HitEffect");
        _contexts.game.SetBulletHitEffect(bulletHitEffectPrefab, null);

        simulationPhaseSystems.Initialize();

        CreateTeam(6, 0, 7, 3);
        CreateTeam(4, 0, 14, 20);
        CreateTeam(8, 1, 7, 40);
        CreateTeam(8, 1, 7, 59);

        var testWallsSystem = new TestGridWallsSystem(_contexts);
        for (var i = 0; i < wallsCount; i++)
        {
            testWallsSystem.Execute();
        }
    }

    private static void CreateTeam(int playersCount, int teamNumber, int spacing, int spawnZ)
    {
        for (var i = 0; i < playersCount; i++)
        {
            var e = CreateRandomUnit(teamNumber);

            e.AddWorldPosition(new Vector3(5 + i * spacing, 0, spawnZ));
        }
    }

    private static GameEntity CreateRandomUnit(int teamNumber)
    {
        var e = GameEntityCreator.CreateEntity();
        switch (Random.Range(0, 3))
        {
            case 0:
                SetupShotgun(e);
                break;
            case 1:
                SetupRifle(e);
                break;
            case 2:
                SetupSniper(e);
                break;
        }

        e.enableCalculateVelocityByPositionChanges = true;
        e.isPlayer                                 = true;
        e.AddTraversalSpeed(1.8f);
        e.AddHealth(200, 200);

        e.vision.directionAngle = teamNumber == 0 ? 0 : 180;
        e.UpdateVision();
        
        e.ReplaceTeamID(teamNumber);
        e.AddTeamColor(teamNumber == 1 ? Color.red : Color.green);
        
        e.hasAI         = teamNumber == 1;
        e.isInteractive = teamNumber != 1;
        return e;
    }

    private static void SetupSniper(GameEntity e)
    {
        e.AddVision(0, 30, 500, 70);
        e.AddViewPrefab("SwatModel/SwatSniper");
        WeaponProvider.GiveSniper(e);
        e.AddReactionDelay(2.25f);
    }

    private static void SetupRifle(GameEntity e)
    {
        e.AddVision(0, 30, 500, 200);
        e.AddViewPrefab("SwatModel/SwatRifle");
        WeaponProvider.GiveRiffle(e);
        e.AddReactionDelay(1.25f);
    }

    private static void SetupShotgun(GameEntity e)
    {
        e.AddVision(0, 30, 500, 400);
        e.AddViewPrefab("SwatModel/SwatShotgun");
        WeaponProvider.GiveShotgun(e);
        e.AddReactionDelay(0.75f);
    }

    public void UpdateSimulation()
    {
        if (timeUntilPhaseEnd >= tickDeltaTime)
        {
            timeUntilPhaseEnd -= tickDeltaTime;
            if (timeUntilPhaseEnd < tickDeltaTime)
            {
                timeUntilPhaseEnd = 0;
                OnSimulationPhaseEnd?.Invoke();
            }

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