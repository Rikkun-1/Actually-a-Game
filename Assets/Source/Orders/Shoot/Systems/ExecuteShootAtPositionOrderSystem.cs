using Entitas;

public class ExecuteShootAtPositionOrderSystem : IExecuteSystem
{
    private readonly IGroup<GameEntity> _entities;

    public ExecuteShootAtPositionOrderSystem(Contexts contexts)
    {
        _entities = contexts.game.GetGroup(GameMatcher.AllOf(GameMatcher.ShootAtPositionOrder,
                                                             GameMatcher.Vision,
                                                             GameMatcher.WorldPosition,
                                                             GameMatcher.TeamID));
    }

    public void Execute()
    {
        foreach (var e in _entities)
        {
            var targetPosition = e.shootAtPositionOrder.position;
            if (AimHelper.IsAimingAtTargetPosition(e, targetPosition))
            {
                ShootHelper.Shoot(e.worldPosition.value, e.vision.directionAngle, e.weapon, e.id.value, e.teamID.value);
            }
        }
    }
}