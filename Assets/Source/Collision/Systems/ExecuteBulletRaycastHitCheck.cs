using System.Collections.Generic;
using Entitas;
using UnityEngine;

public class ExecuteBulletRaycastHitCheck : ReactiveSystem<GameEntity>
{
    private readonly PhysicsContext _physics;
    private static   LayerMask      _layerMask;
    
    public ExecuteBulletRaycastHitCheck(Contexts contexts) : base(contexts.game)
    {
        _physics         = contexts.physics;
        _layerMask.value = LayerMask.GetMask("Default", "Ragdoll");
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.WorldPosition.Added());
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.hasBullet && entity.hasPreviousWorldPosition;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        foreach (var bullet in entities)
        {
            if (!RaycastHelper.Linecast(bullet.previousWorldPosition.value, bullet.worldPosition.value, out var raycastHit, _layerMask)) continue;
            
            var colliderEntity = raycastHit.collider.GetGameEntity();
            if (colliderEntity != null)
            {
                RegisterBulletHit(bullet, colliderEntity, raycastHit);
            }
        }
    }

    private void RegisterBulletHit(GameEntity bullet, GameEntity colliderEntity, RaycastHit raycastHit)
    {
        _physics.CreateEntity()
                .AddBulletHit(bullet.id.value, colliderEntity.id.value, raycastHit);
    }
}