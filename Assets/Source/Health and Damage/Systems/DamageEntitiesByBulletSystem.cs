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
            var firstID  = collisionEntity.collision.firstID;
            var secondID = collisionEntity.collision.secondID;

            var firstEntity  = _game.GetEntityWithId(firstID);
            var secondEntity = _game.GetEntityWithId(secondID);

            if (!CollisionIsCorrect(firstEntity, secondEntity)) continue;

            var bulletEntity =  firstEntity.hasBullet ? firstEntity : secondEntity;
            var hittedEntity = !firstEntity.hasBullet ? firstEntity : secondEntity;

            if (IsSelfHit(hittedEntity, bulletEntity)) continue;
            if (IsHitByTeamMate(hittedEntity, bulletEntity)) continue;

            AddDamageToEntity(hittedEntity, bulletEntity);

            bulletEntity.isDestroyed = true;
        }
    }

    private bool IsHitByTeamMate(GameEntity hitted, GameEntity bullet)
    {
        var shooter = _game.GetEntityWithId(bullet.bullet.shooterID);

        if (shooter is { hasTeamID: false } || !hitted.hasTeamID) return false;

        return shooter != null && shooter.teamID.value == hitted.teamID.value;
    }

    private static bool CollisionIsCorrect(GameEntity firstEntity, GameEntity secondEntity)
    {
        if (firstEntity == null || secondEntity == null)       return false;
        if (firstEntity.hasBullet == secondEntity.hasBullet)   return false;

        return true;
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

    private static bool IsSelfHit(GameEntity shooterEntity, GameEntity bulletEntity)
    {
        return bulletEntity.bullet.shooterID == shooterEntity.id.value;
    }
}