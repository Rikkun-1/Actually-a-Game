using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Entitas;

public class ApplyDamageToHealthSystem : ReactiveSystem<GameEntity>
{
    private readonly Contexts _contexts;

    public ApplyDamageToHealthSystem(Contexts contexts) : base(contexts.game)
    {
        _contexts = contexts;
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.Damage);
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.hasDamage && entity.hasHealth && !entity.isIndestructible;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        foreach (var e in entities)
        {
            var damageSum = e.damage.damageList.Sum(e => e.damage);

            e.ReplaceHealth(e.health.value - damageSum);
        }
    }
}