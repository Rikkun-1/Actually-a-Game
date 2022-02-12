using System;

[Serializable]
public class SimulationController
{
    public float timeForOnePhaseCycle = 5f;
    public float timeUntilPhaseEnd;
    public float tickDeltaTime = 0.02f;

    public event Action OnSimulationPhaseEnd;

    private Contexts               _contexts;
    private SimulationPhaseSystems _simulationPhaseSystems;
    
    public SimulationController(Contexts contexts)
    {
        _contexts              = contexts;
        _simulationPhaseSystems = new SimulationPhaseSystems(contexts);
    }

    public void Initialize(int wallsCount, int windowCount, int coversCount, bool isSpawning)
    {
        _simulationPhaseSystems.Initialize();

        const int spacing      = 4;
        const int playersCount = 6;

        if (!isSpawning) return;
        MapCreator.GenerateMap(_contexts.game, wallsCount, windowCount, coversCount, playersCount, spacing);
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
        _simulationPhaseSystems.Execute();
        _simulationPhaseSystems.Cleanup();
    }

    public void TearDown()
    {
        _simulationPhaseSystems.TearDown();
    }
    
    private void UpdateSimulationTickComponent()
    {
        var previous         = _contexts.game.simulationTick;
        var newTimeFromStart = (float)Math.Round(previous.timeFromStart + tickDeltaTime, 3);

        _contexts.game.ReplaceSimulationTick(tickDeltaTime, previous.tickFromStart + 1, newTimeFromStart);
    }
}