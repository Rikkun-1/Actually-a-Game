using System.Linq;
using Entitas.VisualDebugging.Unity;
using Source;
using UnityEditor;
using UnityEngine;
using static UnityEditor.EditorGUILayout;
using static UnityEngine.GUILayout;


    [CustomEditor(typeof(GameController))]
    public class GameControllerEditor : Editor
    {
        private static GameController     _gameController;
        private static Editor _systemDisablingEditor;

        //private static WorldDebugGrid _worldDebugGrid;
        private static int            _teamID;
        private static Vector2Int     _position;
        private static int            _entityID;

        private void OnEnable()
        {
            _gameController = (GameController)target;
        }

        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            CreateCachedEditor(_gameController._systemDisablingSettings, null, ref _systemDisablingEditor);
            _systemDisablingEditor.OnInspectorGUI();

            LabelField("ID for next created entity", EntityCreator.currentID.ToString());

            if (Button("Create game entity"))
            {
                var entity = EntityCreator.CreateGameEntity();
                Selection.activeGameObject = FindObjectsOfType<EntityBehaviour>()
                                            .Single(e => e.entity == entity).gameObject;
            }

            if (Button("Play simulation phase"))
            {
                var simulationController = _gameController.simulationController;
                if (simulationController.timeUntilPhaseEnd < 0.0001)
                {
                    simulationController.timeUntilPhaseEnd = simulationController.timeForOnePhaseCycle;
                }
            }

            if (Button("Play planning phase"))
            {
                _gameController.UpdatePlanningPhaseSystems();
            }

            _entityID = IntField("Entity ID: ", _entityID);
            _teamID   = IntField("Team ID: ",   _teamID);
            _position = Vector2IntField("Position: ", _position);

            var game = Contexts.sharedInstance.game;
            // if (Button("Show team players positions tactical map"))
            // {
            //     _worldDebugGrid ??= new WorldDebugGrid(20, 20, 1, new Vector3());
            //     var map = TacticalMapCreator.CreateTeamPlayersPositionMap(game, _teamID);
            //     _worldDebugGrid.SetValues(map.ToIntArray());
            // }
            //
            // if (Button("Show amount of team players that can be seen from this position map"))
            // {
            //     _worldDebugGrid ??= new WorldDebugGrid(20, 20, 1, new Vector3());
            //     var map = TacticalMapCreator.CreateAmountOfTeamPlayersThatCanBeSeenFromThisPositionMap(game, _teamID);
            //     _worldDebugGrid.SetValues(map.ToIntArray());
            // }
            //
            // if (Button("Show distance from this position to all positions map"))
            // {
            //     _worldDebugGrid ??= new WorldDebugGrid(20, 20, 1, new Vector3());
            //     var map = TacticalMapCreator.CreateDistanceFromThisPositionToAllPositionsMap(game, _position);
            //     _worldDebugGrid.SetValues(map.ToIntArray());
            // }
            //
            // if (Button("AI"))
            // {
            //     _position = game.GetEntityWithId(_entityID).gridPosition.value;
            //
            //     _worldDebugGrid ??= new WorldDebugGrid(20, 20, 1, new Vector3());
            //     var amountOfPossibleEnemiesMap =
            //         TacticalMapCreator.CreateAmountOfTeamPlayersThatCanBeSeenFromThisPositionMap(game, _teamID);
            //
            //     var distanceToPositionsMap =
            //         TacticalMapCreator.CreateDistanceFromThisPositionToAllPositionsMap(game, _position);
            //
            //     var positionsWhereICanShootSomebody = amountOfPossibleEnemiesMap >= 1;
            //
            //     var distanceToPositionsWhereICanShootSomebody = positionsWhereICanShootSomebody * distanceToPositionsMap;
            //
            //     _worldDebugGrid.SetValues(distanceToPositionsWhereICanShootSomebody.ToIntArray());
            // }
        }
    }
