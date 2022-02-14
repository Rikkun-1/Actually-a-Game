using System.Collections.Generic;
using Entitas;
using Unity.Collections;
using UnityEngine;

public class ExecuteBulletRaycastHitCheck : ReactiveSystem<GameEntity>
{
    private readonly PhysicsContext _physics;
    private readonly LayerMask      _layerMask;

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
        var count = entities.Count;
        var from  = new Vector3[count];
        var to    = new Vector3[count];

        for (var i = 0; i < count; i++)
        {
            from[i] = entities[i].previousWorldPosition.value;
            to[i]   = entities[i].worldPosition.value;
        }

        var results = RaycastHelper.BatchedRaycast(from, to, _layerMask);
        ProcessRaycastResults(entities, results);
    }

    private void ProcessRaycastResults(List<GameEntity> entities, NativeArray<RaycastHit> results)
    {
        for (var i = 0; i < entities.Count; i++)
        {
            var raycastHit = results[i];
            if (raycastHit.collider == null) continue;

            var colliderEntity = raycastHit.collider.GetGameEntity();
            if (colliderEntity != null)
            {
                RegisterBulletHit(entities[i], colliderEntity, raycastHit);
            }
        }
    }

    private void RegisterBulletHit(GameEntity bullet, GameEntity colliderEntity, RaycastHit raycastHit)
    {
        _physics.CreateEntity()
                .AddBulletHit(bullet.id.value, colliderEntity.id.value, raycastHit);
    }
}