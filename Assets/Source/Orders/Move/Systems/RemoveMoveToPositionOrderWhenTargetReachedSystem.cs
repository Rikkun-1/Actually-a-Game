using Entitas;

public class RemoveMoveToPositionOrderWhenTargetReachedSystem : ICleanupSystem
{
    private readonly IGroup<GameEntity> _gameEntities;

    public RemoveMoveToPositionOrderWhenTargetReachedSystem(Contexts contexts)
    {
        _gameEntities = contexts.game.GetGroup(GameMatcher.AllOf(GameMatcher.GridPosition,
                                                                 GameMatcher.MoveToPositionOrder));
    }

    public void Cleanup()
    {
        foreach (var e in _gameEntities.GetEntities())
        {
            if (e.gridPosition.value == e.moveToPositionOrder.position)
            {
                e.RemoveMoveToPositionOrder();
            }
        }
    }
}