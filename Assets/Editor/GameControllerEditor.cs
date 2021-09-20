using System.Linq;
using Entitas.VisualDebugging.Unity;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(GameController))]
public class GameControllerEditor : Editor
{
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
    }
}