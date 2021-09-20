using Entitas.Unity;
using UnityEngine;

public class CollisionTrigger : MonoBehaviour
{
    public void OnTriggerEnter(Collider other)
    {
        var firstEntity = (GameEntity)GetComponentInParent<EntityLink>().entity;
        var firstID     = firstEntity.id.value;

        var secondEntity = (GameEntity)other.GetComponentInParent<EntityLink>().entity;
        var secondID     = secondEntity.id.value;

        if (firstEntity.hasBullet && firstEntity.bullet.shooterID == secondID) return;
        if (secondEntity.hasBullet && secondEntity.bullet.shooterID == firstID) return;

        if (!firstEntity.hasBullet || !secondEntity.hasBullet)
        {
            var e = EntityCreator.CreateGameEntity();
            e.AddCollision(firstID, secondID);
        }
    }
}