using System.Collections.Generic;
using Data;
using GraphProcessor;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public BaseGraph               AIGraph;
    public SimulationController    simulationController;
    public SystemDisablingSettings systemDisablingSettings;
    public Vector2Int              defaultGridSize = new Vector2Int(10, 10);
    public int                     wallsCount;
    public int                     playersCount;

    private Contexts                  _contexts;
    private EachFrameExecutionSystems _eachFrameExecutionSystems;
    private PlanningPhaseSystems      _planningPhaseSystems;

    private void Start()
    {
        Contexts.sharedInstance    = new Contexts();
        _contexts                  = Contexts.sharedInstance;
        _eachFrameExecutionSystems = new EachFrameExecutionSystems(_contexts);
        simulationController       = new SimulationController(_contexts);
        _planningPhaseSystems      = new PlanningPhaseSystems(_contexts);

        DeactivateSystems(systemDisablingSettings.deactivatedSystems);

        _contexts.game.SetAIGraph(AIGraph);
        _contexts.game.SetSimulationTick(0, 0, 0);
        _contexts.game.SetGridSize(defaultGridSize);

        simulationController.Initialize(wallsCount, playersCount);
        _eachFrameExecutionSystems.Initialize();
        simulationController.UpdateSimulation();

        _planningPhaseSystems.Initialize();
    }

    private void Update()
    {
        Time.timeScale = simulationController.timeUntilPhaseEnd > 0  ? 1 : 0;
        _eachFrameExecutionSystems.Execute();
        _eachFrameExecutionSystems.Cleanup();
    }

    private void FixedUpdate()
    {
        simulationController.UpdateSimulation();
    }

    private void OnDestroy()
    {
        simulationController.TearDown();
        _eachFrameExecutionSystems.TearDown();
    }

    private void DeactivateSystems(List<string> deactivatedSystems)
    {
        var systemInfos = simulationController.simulationPhaseSystems.childSystemInfos;

        foreach (var systemInfo in systemInfos)
        {
            if (deactivatedSystems.Contains(systemInfo.systemName))
            {
                systemInfo.isActive = false;
            }
        }
    }

    public void UpdatePlanningPhaseSystems()
    {
        _planningPhaseSystems.Execute();
    }
}