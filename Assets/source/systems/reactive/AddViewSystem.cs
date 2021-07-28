﻿using System.Collections.Generic;
using UnityEngine;
using Entitas;
using Entitas.Unity;

public class AddViewSystem : ReactiveSystem<GameEntity>
{
    readonly Transform parent;
    Contexts contexts;

    public AddViewSystem(Contexts contexts) : base(contexts.game)
    {
        parent = new GameObject("Views").transform;
        this.contexts = contexts;
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.ViewPrefab.Added());
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.hasViewPrefab && !entity.hasUnityView;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        foreach (var e in entities)
        {
            loadViewFromPrefab(e, e.viewPrefab.name);
        }
    }

    void loadViewFromPrefab(GameEntity entity, string prefabName)
    { 
        var viewPrefab = Resources.Load<GameObject>("Prefabs/" + prefabName);
        var viewGameObject = GameObject.Instantiate(viewPrefab, parent);

        viewGameObject.Link(entity);
        entity.AddUnityView(viewGameObject);

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