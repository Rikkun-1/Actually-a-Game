using System.Collections.Generic;
using Entitas.Unity;
using UnityEngine;

public static class UnityViewHelper
{
    public static void DestroyView(GameEntity entity)
    {
        var view = entity.unityView.gameObject;
        UnregisterEventListeners(view);
        view.gameObject.Unlink();
        Object.Destroy(view);
    }

    public static void LoadViewFromPrefab(GameEntity entity, string prefabName, GameObject parent)
    {
        var viewPrefab = Resources.Load<GameObject>(prefabName);

        if (viewPrefab == null)
        {
            Debug.LogWarning($"prefab {prefabName} doesn't exist");
            return;
        }

        CreateNewView(entity, parent, viewPrefab);
    }

    private static void CreateNewView(GameEntity entity, GameObject parent, GameObject viewPrefab)
    {
        var viewGameObject = Object.Instantiate(viewPrefab, parent.transform);
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