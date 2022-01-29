using System.Collections.Generic;
using UnityEngine;
using Entitas;

public class PushBodiesThatHitByBullet : ReactiveSystem<GameEntity>
{
    private readonly Contexts _contexts;

    public PushBodiesThatHitByBullet(Contexts contexts) : base(contexts.game)
    {
        _contexts = contexts;
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.BulletHit);
    }

    protected override bool Filter(GameEntity entity)
    {
        return true;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        const int pushForceMultiplier = 3;

        foreach (var e in entities)
        {
            var damage = _contexts.game.GetEntityWithId(e.bulletHit.bulletEntityID).bullet.damage;
            
            var raycastHit = e.bulletHit.raycastHit;
            var rigidbody  = raycastHit.collider.attachedRigidbody;

            if (rigidbody)
            {
                rigidbody.AddForceAtPosition(-raycastHit.normal * damage * pushForceMultiplier,
                                             raycastHit.point,
                                             ForceMode.Impulse);
            }
        }
    }
}