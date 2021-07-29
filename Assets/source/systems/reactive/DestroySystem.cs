using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entitas;
using Entitas.Unity;

public class DestroySystem : ReactiveSystem<GameEntity>
{

    public DestroySystem(Contexts contexts) : base(contexts.game)
    {
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.Destroyed);
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.isDestroyed;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        foreach (var e in entities)
        {
            if (e.hasUnityView)
            {
                e.unityView.value.gameObject.Unlink();
                GameObject.Destroy(e.unityView.value);
            }

            e.Destroy();
        }
    }
}