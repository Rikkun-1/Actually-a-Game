using Entitas;

public class TargetLostSystem : IExecuteSystem
{
    private readonly IGroup<GameEntity> _entities;
    private readonly GameContext        _game;

    public TargetLostSystem(Contexts contexts)
    {
        _game = contexts.game;
        _entities = _game.GetGroup(GameMatcher.AllOf(GameMatcher.ShootOrder,
                                                     GameMatcher.Vision));
    }

    public void Execute()
    {
        foreach (var e in _entities.GetEntities())
        {
            if (e.shootOrder.target.targetType != TargetType.Entity) return;

            var targetEntity = GetTargetEntity(e);
            if (TargetLost(targetEntity, e))
            {
                e.RemoveShootOrder();
                
                if (e.hasAITarget) e.ReplaceLookOrder(e.aITarget.value);
                else               e.RemoveLookOrder();
            }
        }
    }

    private static bool TargetLost(GameEntity targetEntity, GameEntity e)
    {
        return targetEntity == null || targetEntity.isDestroyed || !RaycastHelper.IsInClearVision(e, targetEntity);
    }

    private GameEntity GetTargetEntity(GameEntity e)
    {
        var targetEntityID = e.shootOrder.target.entityID;
        var targetEntity   = _game.GetEntityWithId(targetEntityID);
        return targetEntity;
    }
}