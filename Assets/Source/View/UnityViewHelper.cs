using Entitas.Unity;
using UnityEngine;

public static class UnityViewHelper
{
    public static void DestroyView(GameEntity entity)
    {
        var eventListeners = entity.unityView.gameObject.gameObject.GetComponents<IEventListener>();
        foreach (var listener in eventListeners)
        {
            listener.UnregisterEventListeners();
        }

        entity.unityView.gameObject.gameObject.Unlink();
        Object.Destroy(entity.unityView.gameObject);
    }

    public static void LoadViewFromPrefab(GameEntity entity, string prefabName, GameObject parent)
    {
        var viewPrefab     = Resources.Load<GameObject>("Prefabs/" + prefabName);
        var viewGameObject = Object.Instantiate(viewPrefab, parent.transform);

        viewGameObject.Link(entity);
        entity.ReplaceUnityView(viewGameObject);

        if (viewGameObject != null)
        {
            var eventListeners = viewGameObject.GetComponents<IEventListener>();
            foreach (var listener in eventListeners)
            {
                listener.RegisterEventListeners(entity);
            }
        }
    }
}