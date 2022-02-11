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

    public void Initialize(int wallsCount, int windowCount, int coversCount, bool isSpawning)
    {
        var bulletHitEffectPrefab = Resources.Load<ParticleSystem>("Effects/Weapon Effects/Prefabs/HitEffect");
        _contexts.game.SetBulletHitEffect(bulletHitEffectPrefab, null);

        simulationPhaseSystems.Initialize();

        const int spacing      = 4;
        const int playersCount = 5;

        if (isSpawning)
        {
            PopulateMapWithFloor(_contexts.game.gridSize.value);
            CreateTeam(playersCount, 0, spacing, 1);
            // CreateTeam(playersCount, 0, spacing, 20);
            // CreateTeam(playersCount, 0, spacing, 50);
            // CreateTeam(playersCount, 0, spacing, 60);
            // CreateTeam(playersCount, 0, spacing, 90);
            
            CreateTeam(playersCount, 1, spacing, 19);
            // CreateTeam(playersCount, 1, spacing, 40);
            // CreateTeam(playersCount, 1, spacing, 70);
            // CreateTeam(playersCount, 1, spacing, 80);
            // CreateTeam(playersCount, 1, spacing, 100);
    
            RandomMapGenerator.PlaceRandomWalls(_contexts.game, wallsCount);
            RandomMapGenerator.PlaceRandomWindows(_contexts.game, windowCount);
            RandomMapGenerator.PlaceRandomCovers(_contexts.game, coversCount);
        }
        // simulationPhaseSystems.Execute();
    }
    
    private static void PopulateMapWithFloor(Vector2Int size)
    {
        for (var x = 0; x < size.x; x++)
        {
            for (var z = 0; z < size.y; z++)
            {
                var e = GameEntityCreator.CreateEntity();
                e.AddWorldPosition(new Vector3(x, 0, z));
                e.AddViewPrefab("Prefabs/floor");
            }
        }
    }

    private static void CreateTeam(int playersCount, int teamNumber, int spacing, int spawnZ)
    {
        for (var i = 0; i < playersCount; i++)
        {
            var position = new Vector2Int(5 + i * spacing, spawnZ);
            CreateRandomUnit(teamNumber, position);
        }
    }

    private static GameEntity CreateRandomUnit(int teamNumber, Vector2Int position)
    {
        var e = GameEntityCreator.CreateEntity(position);
        switch (Random.Range(0, 3))
        {
            case 0: SetupShotgun(e); break;
            case 1: SetupRifle(e);   break;
            case 2: SetupSniper(e);  break;
        }

        e.enableCalculateVelocityByPositionChanges = true;
        e.isPlayer                                 = true;
        e.AddTraversalSpeed(1.8f);
        e.AddHealth(200, 200);

        e.vision.directionAngle = teamNumber == 0 ? 0 : 180;
        e.UpdateVision();
        
        e.ReplaceTeamID(teamNumber);
        e.AddTeamColor(teamNumber == 1 ? Color.red : Color.green);
        
        //e.hasAI         = teamNumber == 1;
        e.hasAI         = true;
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
        if (!(timeUntilPhaseEnd >= tickDeltaTime)) return;
        
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