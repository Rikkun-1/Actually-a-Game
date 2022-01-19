using System.Linq;
using Entitas.VisualDebugging.Unity;
using UnityEditor;
using static UnityEditor.EditorGUILayout;
using static UnityEngine.GUILayout;


[CustomEditor(typeof(GameController))]
public class GameControllerEditor : Editor
{
    private GameController _gameController;
    private Editor         _systemDisablingEditor;

    private void OnEnable()
    {
        _gameController = (GameController)target;
    }

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        CreateCachedEditor(_gameController.systemDisablingSettings, null, ref _systemDisablingEditor);
        _systemDisablingEditor.OnInspectorGUI();

        LabelField("ID for next created entity", EntityCreator.currentID.ToString());

        if (Button("Create game entity"))    CreateGameEntity();
        if (Button("Play simulation phase")) PlaySimulationPhase();
        if (Button("Play planning phase"))   PlayPlaningPhase();
    }

    private void PlayPlaningPhase()
    {
        _gameController.UpdatePlanningPhaseSystems();
    }

    private void PlaySimulationPhase()
    {
        var simulationController = _gameController.simulationController;
        if (simulationController.timeUntilPhaseEnd < 0.0001)
        {
            simulationController.timeUntilPhaseEnd = simulationController.timeForOnePhaseCycle;
        }
    }

    private static void CreateGameEntity()
    {
        var entity = EntityCreator.CreateGameEntity();
        Selection.activeGameObject = FindObjectsOfType<EntityBehaviour>()
                                    .Single(e => e.entity == entity).gameObject;
    }
}