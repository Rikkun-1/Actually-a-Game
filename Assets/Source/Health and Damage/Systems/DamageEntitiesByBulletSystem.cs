using System.Collections.Generic;
using System.Linq;
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
        return context.CreateCollector(GameMatcher.BulletHit.Added());
    }

    protected override bool Filter(GameEntity entity)
    {
        return true;
    }

    protected override void Execute(List<GameEntity> bulletHitEntities)
    {
        DropCollisionsBetweenBullets(ref bulletHitEntities);

        foreach (var bulletHitEntity in bulletHitEntities)
        {
            var bulletHit = bulletHitEntity.bulletHit;
            
            var bulletEntity   = _game.GetEntityWithId(bulletHit.bulletEntityID);
            var colliderEntity = _game.GetEntityWithId(bulletHit.colliderEntityID);

            if (!IsAllowed(bulletEntity, colliderEntity)) continue;

            AddDamageToEntity(colliderEntity, bulletEntity);

            bulletEntity.isDestroyed = true;
        }
    }

    private static bool IsAllowed(GameEntity bulletEntity, GameEntity colliderEntity)
    {
        if (bulletEntity == null || colliderEntity == null) return false;
        if (BulletHelper.IsSelfHit(colliderEntity, bulletEntity)) return false;
        if (BulletHelper.IsHitByTeammate(colliderEntity, bulletEntity)) return false;
        return true;
    }

    private void DropCollisionsBetweenBullets(ref List<GameEntity> bulletHitEntities)
    {
        bulletHitEntities = bulletHitEntities.Where(e => _game.GetEntityWithId(e.bulletHit.colliderEntityID)
                                                              .hasBullet == false)
                                             .ToList();
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