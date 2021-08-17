using System.Collections.Generic;
using Entitas;
using Entitas.Unity;
using UnityEngine;

public class UnityViewSystem : ReactiveSystem<GameEntity>, ICleanupSystem
{
    private readonly IGroup<GameEntity> _destroyedEntities;
    private readonly GameObject _parent;

    public UnityViewSystem(Contexts contexts) : base(contexts.game)
    {
        _parent = new GameObject("Views");
        _destroyedEntities =
            contexts.game.GetGroup(GameMatcher.AllOf(GameMatcher.Destroyed, GameMatcher.UnityView));
    }

    public void Cleanup()
    {
        foreach (var e in _destroyedEntities) DestroyView(e);
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.ViewPrefab.AddedOrRemoved());
    }

    protected override bool Filter(GameEntity entity)
    {
        return true;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        foreach (var e in entities)
        {
            if (e.hasUnityView)
            {
                DestroyView(e);
            }

            if (e.hasViewPrefab)
            {
                LoadViewFromPrefab(e, e.viewPrefab.PrefabName);
            }
        }
    }

    private void DestroyView(GameEntity entity)
    {
        var eventListeners = entity.unityView.GameObject.gameObject.GetComponents<IEventListener>();
        foreach (var listener in eventListeners) listener.UnregisterEventListeners();

        entity.unityView.GameObject.gameObject.Unlink();
        Object.Destroy(entity.unityView.GameObject);
    }

    private void LoadViewFromPrefab(GameEntity entity, string prefabName)
    {
        var viewPrefab = Resources.Load<GameObject>("Prefabs/" + prefabName);
        var viewGameObject = Object.Instantiate(viewPrefab, _parent.transform);

        viewGameObject.Link(entity);
        entity.ReplaceUnityView(viewGameObject);

        if (viewGameObject != null)
        {
            var eventListeners = viewGameObject.GetComponents<IEventListener>();
            foreach (var listener in eventListeners) listener.RegisterEventListeners(entity);
        }
    }
}