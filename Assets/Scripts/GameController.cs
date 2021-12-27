using System.Collections.Generic;
using Data;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public SystemDisablingSettings _systemDisablingSettings;

    public  Vector2Int                _defaultGridSize = new Vector2Int(10, 10);
    public  SimulationController      simulationController;
    private Contexts                  _contexts;
    private EachFrameExecutionSystems _eachFrameExecutionSystems;
    private PlanningPhaseSystems      _planningPhaseSystems;

    private void Start()
    {
        _contexts                  = Contexts.sharedInstance;
        _eachFrameExecutionSystems = new EachFrameExecutionSystems(_contexts);
        simulationController       = new SimulationController(_contexts);
        _planningPhaseSystems      = new PlanningPhaseSystems(_contexts);

        DeactivateSystems(_systemDisablingSettings.deactivatedSystems);

        _contexts.game.SetSimulationTick(0, 0, 0);
        _contexts.game.SetGridSize(_defaultGridSize);

        simulationController.Initialize();
        _eachFrameExecutionSystems.Initialize();

        _planningPhaseSystems.Initialize();
    }

    private void Update()
    {
        simulationController.UpdateSimulation(Time.deltaTime);

        _eachFrameExecutionSystems.Execute();
        _eachFrameExecutionSystems.Cleanup();
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