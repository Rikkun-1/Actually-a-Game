using System.Collections.Generic;
using System.Linq;
using Entitas;
using UnityEngine;

public class PlaceBulletHitEffectsSystem : ReactiveSystem<GameEntity>, IInitializeSystem
{
    private readonly GameContext _game;

    public PlaceBulletHitEffectsSystem(Contexts contexts) : base(contexts.game)
    {
        _game = contexts.game;
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.BulletHit);
    }

    protected override bool Filter(GameEntity entity)
    {
        return true;
    }

    public void Initialize()
    {
        var bulletHitPrefab = _game.bulletHitEffect.bulletHitPrefab;
        _game.ReplaceBulletHitEffect(bulletHitPrefab, Object.Instantiate(bulletHitPrefab));
    }

    protected override void Execute(List<GameEntity> entities)
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