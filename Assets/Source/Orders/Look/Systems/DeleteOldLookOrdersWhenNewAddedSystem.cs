using Entitas;

public class DeleteOldLookOrdersWhenNewAddedSystem : IExecuteSystem
{
    private readonly ICollector<GameEntity> _newLookAtDirection;
    private readonly ICollector<GameEntity> _newLookAtPosition;
    private readonly ICollector<GameEntity> _newLookAtEntity;

    public DeleteOldLookOrdersWhenNewAddedSystem(Contexts contexts)
    {
        _newLookAtDirection = contexts.game.CreateCollector(GameMatcher.LookAtDirectionOrder.Added());
        _newLookAtPosition  = contexts.game.CreateCollector(GameMatcher.LookAtPositionOrder.Added());
        _newLookAtEntity    = contexts.game.CreateCollector(GameMatcher.LookAtEntityOrder.Added());
    }

    public void Execute()
    {
        foreach (var e in _newLookAtEntity.collectedEntities)
        {
            if (!e.hasLookAtEntityOrder)   continue;
            if (e.hasLookAtDirectionOrder) e.RemoveLookAtDirectionOrder();
            if (e.hasLookAtPositionOrder)  e.RemoveLookAtPositionOrder();
        }

        foreach (var e in _newLookAtPosition.collectedEntities)
        {
            if (!e.hasLookAtPositionOrder) continue;
            if (e.hasLookAtDirectionOrder) e.RemoveLookAtDirectionOrder();
            if (e.hasLookAtEntityOrder)    e.RemoveLookAtEntityOrder();
        }

        foreach (var e in _newLookAtDirection.collectedEntities)
        {
            if (!e.hasLookAtDirectionOrder) continue;
            if (e.hasLookAtPositionOrder)   e.RemoveLookAtPositionOrder();
            if (e.hasLookAtEntityOrder)     e.RemoveLookAtEntityOrder();
        }

        _newLookAtDirection.ClearCollectedEntities();
        _newLookAtPosition.ClearCollectedEntities();
        _newLookAtEntity.ClearCollectedEntities();
    }
}