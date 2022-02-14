using Entitas;

public class TargetLostSystem : IExecuteSystem
{
    private readonly IGroup<GameEntity> _entities;
    private readonly GameContext        _game;

    public TargetLostSystem(Contexts contexts)
    {
        _game = contexts.game;
        _entities = _game.GetGroup(GameMatcher.AllOf(GameMatcher.LookAtEntityOrder,
                                                     GameMatcher.ShootAtEntityOrder,
                                                     GameMatcher.Vision));
    }

    public void Execute()
    {
        foreach (var e in _entities.GetEntities())
        {
            var targetEntity = GetTargetEntity(e);
            if (targetEntity == null || targetEntity.isDestroyed || !RaycastHelper.IsInClearVision(e, targetEntity))
            {
                TargetLost(e);
            }
        }
    }

    private GameEntity GetTargetEntity(GameEntity e)
    {
        var targetEntityID = e.shootAtEntityOrder.targetID;
        var targetEntity   = _game.GetEntityWithId(targetEntityID);
        return targetEntity;
    }

    private static void TargetLost(GameEntity e)
    {
        e.RemoveShootAtEntityOrder();
        e.RemoveLookAtEntityOrder();
    }
}