using System.Collections.Generic;
using Entitas;

public class ExecuteBulletRaycastHitCheck : ReactiveSystem<GameEntity>
{
    private readonly Contexts _contexts;
    
    public ExecuteBulletRaycastHitCheck(Contexts contexts) : base(contexts.game)
    {
        _contexts = contexts;
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
        foreach (var e in entities)
        {
            if (RaycastHelper.Raycast(e.previousWorldPosition.value, e.worldPosition.value, out var raycastHit))
            {
                var colliderEntity = raycastHit.collider.GetGameEntity();

                if (colliderEntity != null)
                {

                    _contexts.physics.CreateEntity()
                             .AddBulletHit(e.id.value, colliderEntity.id.value, raycastHit);
                }
            }
        }
    }
}