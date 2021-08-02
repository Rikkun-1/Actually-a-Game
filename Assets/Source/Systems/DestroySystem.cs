using UnityEngine;
using Entitas;
using Entitas.Unity;

public class DestroySystem : ICleanupSystem
{
    readonly Contexts contexts;
    readonly IGroup<GameEntity> entities;

    public DestroySystem(Contexts contexts)
    {
        this.contexts = contexts;
        this.entities = this.contexts.game.GetGroup(GameMatcher.Destroyed);
    }

    public void Cleanup()
    {
        foreach (var e in this.entities.GetEntities())
        {
            if (e.hasUnityView)
            {
                e.unityView.gameObject.gameObject.Unlink();
                GameObject.Destroy(e.unityView.gameObject);
            }

            e.Destroy();
        }
    }
}