using System.Collections.Generic;
using System.Linq;
using Entitas;

public class ApplyDamageToHealthSystem : ReactiveSystem<GameEntity>
{
    public ApplyDamageToHealthSystem(Contexts contexts) : base(contexts.game)
    {
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.Damage);
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.hasDamage &&
               entity.hasHealth &&
               !entity.isIndestructible;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        foreach (var e in entities)
        {
            var damageSum = e.damage.damageList.Sum(elem => elem.damage);

            e.ReplaceHealth(e.health.value - damageSum);
        }
    }
}