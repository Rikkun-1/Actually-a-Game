using Entitas;

public class ExecuteShootAtEntityOrderSystem : IExecuteSystem
{
    private readonly GameContext        _game;
    private readonly IGroup<GameEntity> _entities;

    public ExecuteShootAtEntityOrderSystem(Contexts contexts)
    {
        _game = contexts.game;
        _entities = contexts.game.GetGroup(GameMatcher.AllOf(GameMatcher.ShootAtEntityOrder,
                                                             GameMatcher.Vision,
                                                             GameMatcher.WorldPosition,
                                                             GameMatcher.TeamID,
                                                             GameMatcher.Weapon));
    }

    public void Execute()
    {
        foreach (var e in _entities.GetEntities())
        {
            var targetEntityID = e.shootAtEntityOrder.targetID;
            var targetEntity   = _game.GetEntityWithId(targetEntityID);
            if (targetEntity == null)
            {
                e.RemoveShootAtEntityOrder();
                continue;
            }
    
            if (AimHelper.IsAimingAtTargetEntity(e, targetEntity))
            {
                ShootHelper.Shoot(e.worldPosition.value, e.vision.directionAngle, e.weapon, e.id.value, e.teamID.value);
            }
        }
    }
}