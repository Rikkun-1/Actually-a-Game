using Entitas;

public class ExecuteShootAtEntityOrderSystem : IExecuteSystem
{
    private readonly IGroup<GameEntity> _entities;
    private readonly IGroup<GameEntity> _entitiesThatDontLookAtProperEntity;
    private readonly GameContext        _game;

    public ExecuteShootAtEntityOrderSystem(Contexts contexts)
    {
        _game = contexts.game;
        _entities = contexts.game.GetGroup(GameMatcher.AllOf(GameMatcher.ShootAtEntityOrder,
                                                             GameMatcher.Vision,
                                                             GameMatcher.WorldPosition));

        _entitiesThatDontLookAtProperEntity = contexts.game.GetGroup(GameMatcher.AllOf(GameMatcher.ShootAtEntityOrder,
                                                                                       GameMatcher.Vision,
                                                                                       GameMatcher.WorldPosition)
                                                                                .NoneOf(GameMatcher.LookAtEntityOrder));
    }

    public void Execute()
    {
        foreach (var e in _entitiesThatDontLookAtProperEntity.GetEntities())
        {
            e.AddLookAtEntityOrder(e.shootAtEntityOrder.targetID);
        }

        foreach (var e in _entities)
        {
            if (e.lookAtEntityOrder.targetID != e.shootAtEntityOrder.targetID)
            {
                e.ReplaceLookAtEntityOrder(e.shootAtEntityOrder.targetID);
            }

            var targetEntityID = e.shootAtEntityOrder.targetID;
            var targetEntity   = _game.GetEntityWithId(targetEntityID);
            if (targetEntity == null) continue;

            if (AimHelper.IsAimingAtTargetEntity(e, targetEntity))
            {
                ShootHelper.Shoot(e.worldPosition.value, e.vision.directionAngle, e.weapon, e.id.value);
            }
        }
    }
}