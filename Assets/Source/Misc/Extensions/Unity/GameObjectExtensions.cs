using Entitas.Unity;
using UnityEngine;

internal static class GameObjectExtensions
{
    public static GameEntity GetGameEntity(this Component component)
    {
        return (GameEntity)component.GetComponentInParent<EntityLink>()?.entity;
    }
}