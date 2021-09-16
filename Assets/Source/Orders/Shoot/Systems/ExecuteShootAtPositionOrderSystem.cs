using Entitas;

public class ExecuteShootAtPositionOrderSystem : IExecuteSystem
{
    private readonly Contexts           _contexts;
    private readonly IGroup<GameEntity> _entities;
    private readonly IGroup<GameEntity> _entitiesThatDontLookAtProperPosition;

    public ExecuteShootAtPositionOrderSystem(Contexts contexts)
    {
        _contexts = contexts;
        _entities = contexts.game.GetGroup(GameMatcher.AllOf(GameMatcher.ShootAtPositionOrder,
                                                             GameMatcher.Vision,
                                                             GameMatcher.WorldPosition));

        _entitiesThatDontLookAtProperPosition = contexts.game.GetGroup(GameMatcher.AllOf(GameMatcher.ShootAtPositionOrder,
                                                                                         GameMatcher.Vision,
                                                                                         GameMatcher.WorldPosition)
                                                                                  .NoneOf(GameMatcher.LookAtPositionOrder));
    }

    public void Execute()
    {
        foreach (var e in _entitiesThatDontLookAtProperPosition.GetEntities())
        {
            e.AddLookAtPositionOrder(e.shootAtPositionOrder.position);
        }

        foreach (var e in _entities)
        {
            if (e.lookAtPositionOrder.position != e.shootAtPositionOrder.position)
            {
                e.ReplaceLookAtPositionOrder(e.shootAtPositionOrder.position);
            }

            var targetPosition = e.shootAtPositionOrder.position;

            if (AimHelper.IsAimingAtTargetPosition(e, targetPosition))
            {
                ShootHelper.Shoot(e.worldPosition.value, e.vision.value.directionAngle, e.weapon.value, e.iD.value);
            }
        }
    }
}