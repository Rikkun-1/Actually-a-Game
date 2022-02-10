using Entitas;

public class ExecuteShootAtEntityOrderSystem : IExecuteSystem
{
    private readonly IGroup<GameEntity> _entities;
    private readonly GameContext        _game;

    public ExecuteShootAtEntityOrderSystem(Contexts contexts)
    {
        _game = contexts.game;
        _entities = contexts.game.GetGroup(GameMatcher.AllOf(GameMatcher.ShootAtEntityOrder,
                                                             GameMatcher.Vision,
                                                             GameMatcher.WorldPosition,
                                                             GameMatcher.TeamID,
                                                             GameMatcher.Weapon,
                                                             GameMatcher.ReactionDelay));
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
                if (!e.hasReactionStartTime)
                {
                    e.AddReactionStartTime(GameTime.timeFromStart);
                }
                
                var reactionIsPassed = GameTime.timeFromStart - e.reactionStartTime.value > e.reactionDelay.value;
                if (reactionIsPassed)
                {
                    ShootHelper.Shoot(e, e.weapon);
                }
            }
        }
    }
}