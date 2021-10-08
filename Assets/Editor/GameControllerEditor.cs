using System.Linq;
using Entitas.VisualDebugging.Unity;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(GameController))]
public class GameControllerEditor : Editor
{
    private        GameController _gameController;
    private static WorldDebugGrid _worldDebugGrid;
    private static int            _teamID;
    private static Vector2Int     _position;

    private void OnEnable()
    {
        _gameController = (GameController)target;
    }

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        EditorGUILayout.LabelField("ID for next created entity", EntityCreator.currentID.ToString());

        if (GUILayout.Button("Create game entity"))
        {
            var entity = EntityCreator.CreateGameEntity();
            Selection.activeGameObject = FindObjectsOfType<EntityBehaviour>()
                                        .Single(e => e.entity == entity).gameObject;
        }

        if (GUILayout.Button("Play simulation phase"))
        {
            var simulationController = _gameController.simulationController;
            if (simulationController.timeUntilPhaseEnd < 0.0001)
            {
                simulationController.timeUntilPhaseEnd = simulationController.timeForOnePhaseCycle;
            }
        }

        var teamID = EditorGUILayout.IntField("Team ID: ", 1);
        
        if (GUILayout.Button("Show team players positions tactical map"))
        {
            _worldDebugGrid ??= new WorldDebugGrid(20, 20, 1, new Vector3());
            _worldDebugGrid.SetValues(TacticalMapCreator.CreateTeamPlayersPositionMap(Contexts.sharedInstance, teamID));
        }
        
        if (GUILayout.Button("Show amount of team players that can be seen from this position map"))
        {
            _worldDebugGrid ??= new WorldDebugGrid(20, 20, 1, new Vector3());
            _worldDebugGrid.SetValues(TacticalMapCreator.CreateAmountOfTeamPlayersThatCanBeSeenFromThisPositionMap(Contexts.sharedInstance, teamID));
        }
    }
}