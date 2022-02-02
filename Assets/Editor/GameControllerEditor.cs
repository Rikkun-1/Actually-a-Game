using System.Linq;
using Entitas.VisualDebugging.Unity;
using UnityEditor;
using static UnityEngine.GUILayout;


[CustomEditor(typeof(GameController))]
public class GameControllerEditor : Editor
{
    private GameController _gameController;

    private void OnEnable()
    {
        _gameController = (GameController)target;
    }

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        if (Button("Create game entity"))  CreateGameEntity();
        if (Button("Play"))                Play();
        if (Button("Play planning phase")) PlayPlaningPhase();
    }

    private void PlayPlaningPhase()
    {
        _gameController.UpdatePlanningPhaseSystems();
    }

    private void Play()
    {
        var simulationController = _gameController.simulationController;
        if (simulationController.timeUntilPhaseEnd < 0.0001)
        {
            simulationController.timeUntilPhaseEnd = simulationController.timeForOnePhaseCycle;
        }
    }

    private static void CreateGameEntity()
    {
        var entity = GameEntityCreator.CreateEntity();
        Selection.activeGameObject = FindObjectsOfType<EntityBehaviour>()
                                    .Single(e => e.entity == entity).gameObject;
    }
}