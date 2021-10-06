using UnityEngine;

public class GameController : MonoBehaviour
{
    public  Vector2Int                _defaultMapSize = new Vector2Int(10, 10);
    private Contexts                  _contexts;
    private EachFrameExecutionSystems _eachFrameExecutionSystems;
    public  SimulationController      simulationController;

    private void Start()
    {
        _contexts                  = Contexts.sharedInstance;
        _eachFrameExecutionSystems = new EachFrameExecutionSystems(_contexts);
        simulationController      = new SimulationController(_contexts);

        _contexts.game.SetSimulationTick(0, 0, 0);
        _contexts.game.SetMapSize(_defaultMapSize);

        simulationController.Initialize();
        _eachFrameExecutionSystems.Initialize();
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
}