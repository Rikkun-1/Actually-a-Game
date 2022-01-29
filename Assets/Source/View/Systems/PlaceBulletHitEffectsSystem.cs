using System.Collections.Generic;
using System.Linq;
using Entitas;
using UnityEngine;

public class PlaceBulletHitEffectsSystem : ReactiveSystem<PhysicsEntity>, IInitializeSystem
{
    private readonly GameContext _game;

    public PlaceBulletHitEffectsSystem(Contexts contexts) : base(contexts.physics)
    {
        _game = contexts.game;
    }

    protected override ICollector<PhysicsEntity> GetTrigger(IContext<PhysicsEntity> context)
    {
        return context.CreateCollector(PhysicsMatcher.BulletHit);
    }

    protected override bool Filter(PhysicsEntity entity)
    {
        return true;
    }

    public void Initialize()
    {
        var bulletHitPrefab = _game.bulletHitEffect.bulletHitPrefab;
        _game.ReplaceBulletHitEffect(bulletHitPrefab, Object.Instantiate(bulletHitPrefab));
    }

    protected override void Execute(List<PhysicsEntity> entities)
    {
        var bulletHitEffect = _game.bulletHitEffect.bulletHitCachedInstance;
        var effectTransform = bulletHitEffect.transform;

        var raycastHits = entities.Select(e => e.bulletHit.raycastHit);
        
        foreach (var raycastHit in raycastHits)
        {
            effectTransform.position = raycastHit.point;
            effectTransform.forward  = raycastHit.normal;

            bulletHitEffect.Emit(1);
        }
    }
}