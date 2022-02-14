using System.Collections.Generic;
using UnityEngine;
using Entitas;

public class PushBodiesThatHitByBullet : ReactiveSystem<PhysicsEntity>
{
    private readonly Contexts _contexts;

    public PushBodiesThatHitByBullet(Contexts contexts) : base(contexts.physics)
    {
        _contexts = contexts;
    }

    protected override ICollector<PhysicsEntity> GetTrigger(IContext<PhysicsEntity> context)
    {
        return context.CreateCollector(PhysicsMatcher.BulletHit);
    }

    protected override bool Filter(PhysicsEntity entity)
    {
        return entity.hasBulletHit;
    }

    protected override void Execute(List<PhysicsEntity> entities)
    {
        const float pushForceMultiplier = 1.5f;

        foreach (var e in entities)
        {
            var damage = _contexts.game.GetEntityWithId(e.bulletHit.bulletEntityID).bullet.damage;
            
            var raycastHit = e.bulletHit.raycastHit;
            var rigidbody  = raycastHit.collider.attachedRigidbody;

            if (rigidbody)
            {
                rigidbody.AddForceAtPosition(-raycastHit.normal * (damage * pushForceMultiplier),
                                             raycastHit.point,
                                             ForceMode.Impulse);
            }
        }
    }
}