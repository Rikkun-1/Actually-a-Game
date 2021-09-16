using System.Collections.Generic;
using Entitas;

public class DamageEntitiesByBulletSystem : ReactiveSystem<GameEntity>
{
    private readonly Contexts _contexts;

    public DamageEntitiesByBulletSystem(Contexts contexts) : base(contexts.game)
    {
        _contexts = contexts;
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.Collision.Added());
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.hasCollision;
    }

    protected override void Execute(List<GameEntity> collisionEntities)
    {
        foreach (var collisionEntity in collisionEntities)
        {
            var firstID  = collisionEntity.collision.firstID;
            var secondID = collisionEntity.collision.secondID;

            var firstCollider  = _contexts.game.GetEntityWithID(firstID);
            var secondCollider = _contexts.game.GetEntityWithID(secondID);

            if (firstCollider == null || secondCollider == null)      continue;
            if (IsNoBulletInCollision(firstCollider, secondCollider)) continue;
            if (firstCollider.hasBullet && secondCollider.hasBullet)  continue;

            var bulletEntity =  firstCollider.hasBullet ? firstCollider : secondCollider;
            var hittedEntity = !firstCollider.hasBullet ? firstCollider : secondCollider;

            if (IsSelfHit(hittedEntity, bulletEntity)) continue;

            AddDamageToEntity(hittedEntity, bulletEntity);
        }
    }

    private static void AddDamageToEntity(GameEntity hittedEntity, GameEntity bulletEntity)
    {
        if (!hittedEntity.hasHealth) return;

        var damage = new Damage(bulletEntity.bullet.shooterID, bulletEntity.bullet.damage);
        
        if (hittedEntity.hasDamage)
        {
            var damageList = hittedEntity.damage.damageList;
            damageList.Add(damage);
            hittedEntity.ReplaceDamage(damageList);
        }
        else
        {
            hittedEntity.AddDamage(new List<Damage> { damage });
        }
    }

    private static bool IsNoBulletInCollision(GameEntity firstCollider, GameEntity secondCollider)
    {
        return !firstCollider.hasBullet && !secondCollider.hasBullet;
    }

    private static bool IsSelfHit(GameEntity shooterEntity, GameEntity bulletEntity)
    {
        return bulletEntity.bullet.shooterID == shooterEntity.iD.value;
    }
}