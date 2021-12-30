using System.Collections.Generic;
using Entitas;

public class DamageEntitiesByBulletSystem : ReactiveSystem<GameEntity>
{
    private readonly GameContext _game;

    public DamageEntitiesByBulletSystem(Contexts contexts) : base(contexts.game)
    {
        _game = contexts.game;
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
            var (firstEntity, secondEntity) = GetCollidedEntities(collisionEntity);

            if (!BulletCollisionHelper.IsCollisionBetweenBulletAndOtherEntity(firstEntity, secondEntity)) continue;

            
            var (bulletEntity, suffererEntity) = BulletCollisionHelper.SplitToBulletAndSufferer(firstEntity, secondEntity);

            if (BulletHelper.IsSelfHit(suffererEntity, bulletEntity)) continue;
            if (BulletHelper.IsHitByTeammate(suffererEntity, bulletEntity)) continue;

            AddDamageToEntity(suffererEntity, bulletEntity);

            bulletEntity.isDestroyed = true;
        }
    }

    private (GameEntity firstEntity, GameEntity secondEntity) GetCollidedEntities(GameEntity collisionEntity)
    {
        var firstID  = collisionEntity.collision.firstID;
        var secondID = collisionEntity.collision.secondID;

        var firstEntity  = _game.GetEntityWithId(firstID);
        var secondEntity = _game.GetEntityWithId(secondID);
        
        return (firstEntity, secondEntity);
    }
    
    private static void AddDamageToEntity(GameEntity suffererEntity, GameEntity bulletEntity)
    {
        if (!suffererEntity.hasHealth) return;

        var damage = new Damage(bulletEntity.bullet.shooterID, bulletEntity.bullet.damage);

        if (suffererEntity.hasDamage)
        {
            var damageList = suffererEntity.damage.damageList;
            damageList.Add(damage);
            suffererEntity.ReplaceDamage(damageList);
        }
        else
        {
            suffererEntity.AddDamage(new List<Damage> { damage });
        }
    }
}