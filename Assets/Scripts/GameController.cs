using GraphProcessor;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public BaseGraph            AIGraph;
    public SimulationController simulationController;
    public Vector2Int           defaultGridSize = new Vector2Int(10, 10);
    public int                  wallsCount;
    public int                  playersCount;
    public float                timeScale;

    private Contexts                  _contexts;
    private EachFrameExecutionSystems _eachFrameExecutionSystems;
    private PlanningPhaseSystems      _planningPhaseSystems;

    public Image timeProgress;

    private void Start()
    {
        UnityViewHelper.ClearCachedResources();
        
        Contexts.sharedInstance                   =  new Contexts();
        _contexts                                 =  Contexts.sharedInstance;
        _eachFrameExecutionSystems                =  new EachFrameExecutionSystems(_contexts);
        simulationController                      =  new SimulationController(_contexts);
        _planningPhaseSystems                     =  new PlanningPhaseSystems(_contexts);
        simulationController.OnSimulationPhaseEnd += UpdatePlanningPhaseSystems;
        simulationController.tickDeltaTime        =  Time.fixedDeltaTime;
        
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
        timeProgress.fillAmount = 1 - simulationController.timeUntilPhaseEnd / simulationController.timeForOnePhaseCycle;
        
        Time.timeScale = simulationController.timeUntilPhaseEnd > 0  ? timeScale : 0;
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

    public void Turn()
    {
        if (simulationController.timeUntilPhaseEnd < 0.0001)
        {
            simulationController.timeUntilPhaseEnd = simulationController.timeForOnePhaseCycle;
        }
    }
    
    public void UpdatePlanningPhaseSystems()
    {
        _planningPhaseSystems.Execute();
    }
}