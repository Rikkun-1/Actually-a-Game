using Entitas;
using Entitas.VisualDebugging.Unity;
using UnityEditor;
using UnityEngine;

public class EntityTemplateSaveLoadWindow : EditorWindow
{
    private string           _assetSaveName  = "";
    private string           _assetLoadName  = "";
    private string           _selectedEntity = "";
    private string           _saveFileName   = "";
    private string           _loadFileName   = "";
    private IEntity          _currentEntity;
    private EntitySaveLoader _entitySaveLoader;

    [MenuItem("Tools/Entity template Save Loader")]
    public static void ShowWindow()
    {
        GetWindow(typeof(EntityTemplateSaveLoadWindow));
    }

    private void OnGUI()
    {
        CheckInit();

        GUILayout.Label("Entity", EditorStyles.boldLabel);

        DisplaySelectedEntity();
        DisplaySaveEntityToTemplateAssetOption();
        DisplayMakeNewEntityFromTemplateOption();
        DisplaySaveGameOption();
        DisplayLoadGameOption();
    }

    private void DisplayLoadGameOption()
    {
        GUILayout.Label("Load Game ", EditorStyles.boldLabel);
        _loadFileName = EditorGUILayout.TextField("loadFileName:", _loadFileName);
        if (GUILayout.Button("Load"))
        {
            _entitySaveLoader.LoadEntitiesFromSaveFile(Contexts.sharedInstance, _loadFileName);
        }
    }

    private void DisplaySaveGameOption()
    {
        GUILayout.Label("Save Game", EditorStyles.boldLabel);
        _saveFileName = EditorGUILayout.TextField("saveFileName:", _saveFileName);
        if (GUILayout.Button("Save all"))
        {
            EntitySaveLoader.SaveAllEntitiesInScene(Contexts.sharedInstance, _saveFileName);
            AssetDatabase.Refresh();
        }
    }

    private void DisplayMakeNewEntityFromTemplateOption()
    {
        GUILayout.Label("Make new Entity from template", EditorStyles.boldLabel);

        _assetLoadName = EditorGUILayout.TextField("Name of template:", _assetLoadName);

        if (GUILayout.Button("Make new entity!"))
        {
            _entitySaveLoader.MakeEntityFromTemplate(_assetLoadName, Contexts.sharedInstance);
        }
    }

    private void DisplaySaveEntityToTemplateAssetOption()
    {
        GUILayout.Label("Save Entity To template asset", EditorStyles.boldLabel);

        _assetSaveName = EditorGUILayout.TextField("Name of template to save:", _assetSaveName);

        if (GUILayout.Button("Save to template!"))
        {
            if (_currentEntity == null) return;

            _entitySaveLoader.SaveEntityTemplateToSingleFile(_currentEntity, _assetSaveName);
            AssetDatabase.Refresh();
            _entitySaveLoader.ReloadTemplates();
        }
    }

    private void DisplaySelectedEntity()
    {
        EditorGUILayout.TextField("selectedEntity", _selectedEntity);

        var activeGameObject = Selection.activeGameObject;
        if (!activeGameObject) return;
        
        _currentEntity = activeGameObject.GetComponent<EntityBehaviour>()?.entity;
        if (_currentEntity == null) return;
        
        _selectedEntity = _currentEntity.ToString();
    }

    private void CheckInit()
    {
        if (_entitySaveLoader != null) return;
        
        _entitySaveLoader = new EntitySaveLoader(new TemplateLoader());
        _entitySaveLoader.ReloadTemplates();
    }
}