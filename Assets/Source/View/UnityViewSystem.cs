using System.Collections.Generic;
using UnityEngine;
using Entitas;
using Entitas.Unity;

public class UnityViewSystem : ReactiveSystem<GameEntity>, ICleanupSystem
{
    readonly GameObject parent;
    readonly Contexts contexts;
    readonly IGroup<GameEntity> destroyedEntities; 

    public UnityViewSystem(Contexts contexts) : base(contexts.game)
    {
        this.parent = new GameObject("Views");
        this.contexts = contexts;
        this.destroyedEntities = this.contexts.game.GetGroup(GameMatcher.Destroyed);
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
                destroyView(e);
            }

            if (e.hasViewPrefab)
            {
                loadViewFromPrefab(e, e.viewPrefab.prefabName);
            }
        }
    }

    public void Cleanup()
    {
        foreach(var e in destroyedEntities)
        {
            destroyView(e);
        }
    }

    void destroyView(GameEntity entity)
    {
        entity.unityView.gameObject.gameObject.Unlink();
        GameObject.Destroy(entity.unityView.gameObject);

        var eventListeners = entity.unityView.gameObject.gameObject.GetComponents<IEventListener>();
        foreach (var listener in eventListeners)
        {
            listener.UnregisterEventListeners();
        }
    }

    void loadViewFromPrefab(GameEntity entity, string prefabName)
    {
        var viewPrefab = Resources.Load<GameObject>("Prefabs/" + prefabName);
        var viewGameObject = GameObject.Instantiate(viewPrefab, this.parent.transform);

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