using Entitas.Unity;
using Source;
using UnityEngine;

public class BulletCollisionTrigger : MonoBehaviour
{
    public void OnTriggerEnter(Collider other)
    {
        var firstEntity  = (GameEntity)GetComponentInParent<EntityLink>().entity;
        var secondEntity = (GameEntity)other.GetComponentInParent<EntityLink>().entity;

        if(!BulletCollisionHelper.IsCollisionBetweenBulletAndOtherEntity(firstEntity, secondEntity)) return;
        
        EntityCreator.CreateGameEntity()
                     .AddCollision(firstEntity.id.value, secondEntity.id.value);
    }
}