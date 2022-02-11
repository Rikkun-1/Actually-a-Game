using System.Collections.Generic;
using UnityEngine;
using Entitas;

public class PushBodiesThatHitByBullet : ReactiveSystem<PhysicsEntity>
{
    private readonly GameContext _game;
    private readonly LayerMask   _ragdollMask;
    private readonly LayerMask   _hitboxLayer;
    
    public PushBodiesThatHitByBullet(Contexts contexts) : base(contexts.physics)
    {
        _game        = contexts.game;
        _ragdollMask = LayerMask.GetMask("Ragdoll");
        _hitboxLayer  = LayerMask.NameToLayer("Hitbox");
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
        const float pushForceMultiplier = 2.5f;

        foreach (var e in entities)
        {
            var damage = _game.GetEntityWithId(e.bulletHit.bulletEntityID).bullet.damage;
            
            var raycastHit = e.bulletHit.raycastHit;
            if (!IsHitHitbox(raycastHit)) continue;
            
            if (!RaycastToRagdoll(raycastHit, out var ragdollHitInfo)) continue;
            
            PushBody(ragdollHitInfo, damage * pushForceMultiplier);
        }
    }

    private static void PushBody(RaycastHit ragdollHitInfo, float force)
    {
        ragdollHitInfo.rigidbody.AddForceAtPosition(-ragdollHitInfo.normal * force, ragdollHitInfo.point, ForceMode.Impulse);
    }

    private bool RaycastToRagdoll(RaycastHit hitboxHit, out RaycastHit ragdollHitInfo)
    {
        var origin = hitboxHit.point + hitboxHit.normal;
        return Physics.Raycast(origin, -hitboxHit.normal, out ragdollHitInfo, 2f, _ragdollMask);
    }

    private bool IsHitHitbox(RaycastHit raycastHit)
    {
        return raycastHit.collider.gameObject.layer == _hitboxLayer;
    }
}