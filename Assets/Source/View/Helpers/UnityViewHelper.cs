using System.Collections.Generic;
using Entitas.Unity;
using UnityEngine;

public static class UnityViewHelper
{
    private static readonly Dictionary<string, GameObject> _cachedResources = new Dictionary<string, GameObject>();

    public static void ClearCachedResources()
    {
        _cachedResources.Clear();
    }
    
    public static void DestroyView(GameEntity entity)
    {
        var view = entity.unityView.gameObject;
        entity.RemoveUnityView();
        
        UnregisterEventListeners(view);
        view.gameObject.Unlink();
        Object.Destroy(view);
    }

    public static void LoadViewFromPrefab(GameEntity entity, string prefabName, GameObject parent)
    {
        if (_cachedResources.TryGetValue(prefabName, out var prefab))
        {
            CreateNewView(entity, parent, prefab);
            return;
        }
        
        var viewPrefab = Resources.Load<GameObject>(prefabName);

        if (viewPrefab == null)
        {
            Debug.LogWarning($"prefab {prefabName} doesn't exist");
            return;
        }

        CreateNewView(entity, parent, viewPrefab);
        _cachedResources[prefabName] = viewPrefab;
    }

    private static void CreateNewView(GameEntity entity, GameObject parent, GameObject viewPrefab)
    {
        var position = entity.hasWorldPosition 
                           ? entity.worldPosition.value
                           : Vector3.zero;

        var viewGameObject = Object.Instantiate(viewPrefab, position, viewPrefab.transform.rotation, parent.transform);
        viewGameObject.Link(entity);
        entity.ReplaceUnityView(viewGameObject);
        RegisterEventListeners(entity, viewGameObject);
    }

    private static void UnregisterEventListeners(GameObject view)
    {
        foreach (var listener in GetEventListeners(view))
        {
            listener.UnregisterEventListeners();
        }
    }

    private static void RegisterEventListeners(GameEntity entity, GameObject view)
    {
        foreach (var listener in GetEventListeners(view))
        {
            listener.RegisterEventListeners(entity);
        }
    }

    private static IEnumerable<IEventListener> GetEventListeners(GameObject view)
    {
        return view.GetComponentsInChildren<IEventListener>();
    }
}