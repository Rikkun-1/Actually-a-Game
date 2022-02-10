using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Entitas;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine;
using Debug = System.Diagnostics.Debug;
using Random = UnityEngine.Random;

public class EntitySaveLoader
{
    private readonly Dictionary<string, EntityTemplate> _templateDictionary = new Dictionary<string, EntityTemplate>();

    private readonly ITemplateLoader _templateLoader;

    private bool _dictionaryReady;
    
    public EntitySaveLoader(ITemplateLoader templateLoader)
    {
        _templateLoader = templateLoader;
    }

    class GameSave
    {
        public EntitiesSaveData entitiesSaveData;
        public Random.State     randomState;
    }

    public static void SaveAllEntitiesInScene(Contexts contexts, string saveFileName)
    {
        var state = JsonConvert.SerializeObject(Random.state);
        foreach (var gameEntity in contexts.game.GetEntities())
        {
            gameEntity.isSavingData = true;
        }

        var savingEntities = contexts.game.GetGroup(GameMatcher.SavingData).GetEntities();
        var saveData       = new EntitiesSaveData();
        foreach (var savingEntity in savingEntities)
        {
            saveData.entityInfos.Add(MakeEntityInfo(savingEntity, null));
        }

        var gameSave = new GameSave {entitiesSaveData = saveData, randomState = Random.state};

        var json = JsonConvert.SerializeObject(gameSave, Formatting.Indented);
        var path = $"Assets/Resources/EntityTemplate/SaveFile/{saveFileName}.json";

        File.WriteAllText(path, json);
    }

    public void LoadEntitiesFromSaveFile(Contexts contexts, string saveFileName)
    {
        var savedTuple = _templateLoader.LoadSavedEntityFile(saveFileName);

        var saveData = JsonConvert.DeserializeObject<GameSave>(savedTuple.Item2);
        foreach (var entityInfo in saveData.entitiesSaveData.entityInfos)
        {
            MakeEntityFromEntityInfo(entityInfo, contexts);
        }
        Random.state = saveData.randomState;
        GameEntityCreator.UpdateCurrentID();
    }

    public IEntity MakeEntityFromTemplate(string templateName, Contexts contexts)
    {
        if (!_dictionaryReady) ReloadTemplates();

        if (!_templateDictionary.ContainsKey(templateName))
        {
            Debug.WriteLine($"can't find name template: {templateName}");
            return null;
        }

        return MakeEntityFromEntityInfo(_templateDictionary[templateName], contexts);
    }

    public void ReloadTemplates()
    {
        _templateDictionary.Clear();
        LoadSingleTemplate();
        LoadTemplateGroups();
        _dictionaryReady = true;
    }

    public IEntity MakeEntityFromJson(string json, Contexts contexts)
    {
        if (string.IsNullOrWhiteSpace(json))
        {
            Debug.WriteLine("empty json!");
        }

        var entityInfo = JsonConvert.DeserializeObject<EntityTemplate>(json);
        return MakeEntityFromEntityInfo(entityInfo, contexts);
    }

    //todo : support input, ui etc... components
    private static IEntity MakeEntityFromEntityInfo(EntityTemplate entityTemplate, Contexts contexts)
    {
        var newEntity = MakeEntityByContext(entityTemplate, contexts);
        AddTagComponents(entityTemplate, newEntity);
        AddComponents(entityTemplate, newEntity);
        return newEntity;
    }

    private static string RemoveComponentSuffix(string nameOfComponent)
    {
        return nameOfComponent.EndsWith("Component") 
                   ? nameOfComponent.Remove(nameOfComponent.Length - 9, 9) 
                   : nameOfComponent;
    }

    private static bool IsFlagComponent(IComponent component)
    {
        var type            = component.GetType();
        var fieldCount      = type.GetFields().Length;
        var propertiesCount = type.GetProperties().Length;
        return fieldCount + propertiesCount == 0;
    }

    /// <summary>
    ///     only value or string field have components serialized.
    ///     ref type components ignored.
    /// </summary>
    private static string MakeEntityInfoJson(IEntity entity, Formatting jsonFormatting, string templateName)
    {
        var entityTemplate = MakeEntityInfo(entity, templateName);
        var jsonStr        = JsonConvert.SerializeObject(entityTemplate, jsonFormatting);

        return jsonStr;
    }

    private static EntityTemplate MakeEntityInfo(IEntity entity, string templateName)
    {
        var entityInfo = new EntityTemplate
        {
            name    = templateName,
            context = entity.contextInfo.name
        };

        foreach (var component in entity.GetComponents())
        {
            if (component.GetType().Name.Contains("Listener")) continue;
            if (IsHaveIgnoreSaveAttribute(component)) continue;

            var componentName = RemoveComponentSuffix(component.GetType().ToString());

            if (IsFlagComponent(component))
            {
                entityInfo.tags += componentName + ",";
            }
            else
            {
                entityInfo.components.Add(componentName, component);
            }
        }

        return entityInfo;
    }

    private void LoadSingleTemplate()
    {
        var tuples = _templateLoader.LoadSingleTemplateFile();
        foreach (var tuple in tuples)
        {
            var json        = tuple.Item2;
            var newTemplate = JsonConvert.DeserializeObject<EntityTemplate>(json);

            if (_templateDictionary.ContainsKey(newTemplate.name))
            {
                throw new Exception();
            }

            _templateDictionary.Add(newTemplate.name, newTemplate);
        }
    }

    private void LoadTemplateGroups()
    {
        var tuples = _templateLoader.LoadGroupTemplateFiles();

        foreach (var tuple in tuples)
        {
            var jObject = JObject.Parse(tuple.Item2);

            foreach (var jPair in jObject)
            {
                var newTemplate = jPair.Value.ToObject<EntityTemplate>();
                Debug.WriteLine(newTemplate.ToString());

                if (_templateDictionary.ContainsKey(jPair.Key))
                {
                    throw new Exception($"already have key {jPair.Key}");
                }

                _templateDictionary.Add(jPair.Key, newTemplate);
            }
        }
    }

    /// <summary>
    ///     make Json file and save to Resource/EntityTemplate.
    /// </summary>
    public void SaveEntityTemplateToSingleFile(IEntity entity, string templateName)
    {
        var json = MakeEntityInfoJson(entity, Formatting.Indented, templateName);
        var path = $"Assets/Resources/EntityTemplate/SingleEntity/{templateName}.json";

        if (!File.Exists(path))
        {
            File.Create(path).Close();
        }

        File.WriteAllText(path, json);
    }

    private static bool IsHaveIgnoreSaveAttribute(object obj)
    {
        var type = obj.GetType();
        return Attribute.IsDefined(type, typeof(IgnoreSaveAttribute));
    }

    /// <summary>
    ///     only make new Entity. not add components
    /// </summary>
    private static IEntity MakeEntityByContext(EntityTemplate entityTemplate, Contexts contexts)
    {
        dynamic context = contexts.allContexts.First(context => context.contextInfo.name == entityTemplate.context);
        return context.CreateEntity();
    }


    private static void AddComponents(EntityTemplate entityTemplate, IEntity newEntity)
    {
        //add components
        //deserialized componentValue is JObject. JObject can be casted with dynamic (ToObject)
        foreach (KeyValuePair<string, dynamic> componentInfo in entityTemplate.components)
        {
            var componentLookUpName = RemoveComponentSuffix(componentInfo.Key);

            if (!GameComponentsLookup.componentNames.Contains(componentLookUpName))
            {
                throw new Exception($"{componentLookUpName} is not in GameComponentsLookup");
            }

            var componentLookUpIndex = Array.IndexOf(GameComponentsLookup.componentNames, componentLookUpName);
            var componentType        = GameComponentsLookup.componentTypes[componentLookUpIndex];
            var component            = componentInfo.Value.ToObject(componentType);

            ((Entity)newEntity).AddComponent(componentLookUpIndex, component as IComponent);
        }
    }

    private static void AddTagComponents(EntityTemplate entityTemplate, IEntity newEntity)
    {
        if (string.IsNullOrEmpty(entityTemplate.tags)) return;

        var parsedTags = entityTemplate.tags.Split(',');

        foreach (var tagName in parsedTags)
        {
            if (string.IsNullOrEmpty(tagName)) continue;

            var componentLookUpName = RemoveComponentSuffix(tagName);

            if (!GameComponentsLookup.componentNames.Contains(componentLookUpName))
            {
                throw new Exception("{componentLookUpName} is not in GameComponentsLookup");
            }

            var componentLookUpIndex = Array.IndexOf(GameComponentsLookup.componentNames, componentLookUpName);
            var componentType        = GameComponentsLookup.componentTypes[componentLookUpIndex];
            var tagComponent         = Activator.CreateInstance(componentType);

            ((Entity)newEntity).AddComponent(componentLookUpIndex, tagComponent as IComponent);
        }
    }
}