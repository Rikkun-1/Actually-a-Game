using Entitas;

public class DeleteOldShootOrdersWhenNewAddedSystem : IExecuteSystem
{
    private readonly ICollector<GameEntity> _newShootAtDirection;
    private readonly ICollector<GameEntity> _newShootAtEntity;
    private readonly ICollector<GameEntity> _newShootAtPosition;

    public DeleteOldShootOrdersWhenNewAddedSystem(Contexts contexts)
    {
        _newShootAtDirection = contexts.game.CreateCollector(GameMatcher.ShootAtDirectionOrder.Added());
        _newShootAtPosition  = contexts.game.CreateCollector(GameMatcher.ShootAtPositionOrder.Added());
        _newShootAtEntity    = contexts.game.CreateCollector(GameMatcher.ShootAtEntityOrder.Added());
    }

    public void Execute()
    {
        foreach (var e in _newShootAtEntity.collectedEntities)
        {
            if (!e.hasShootAtEntityOrder)   continue;
            if (e.hasShootAtDirectionOrder) e.RemoveShootAtDirectionOrder();
            if (e.hasShootAtPositionOrder)  e.RemoveShootAtPositionOrder();
        }

        foreach (var e in _newShootAtPosition.collectedEntities)
        {
            if (!e.hasShootAtPositionOrder) continue;
            if (e.hasShootAtDirectionOrder) e.RemoveShootAtDirectionOrder();
            if (e.hasShootAtEntityOrder)    e.RemoveShootAtEntityOrder();
        }

        foreach (var e in _newShootAtDirection.collectedEntities)
        {
            if (!e.hasShootAtDirectionOrder) continue;
            if (e.hasShootAtPositionOrder)   e.RemoveShootAtPositionOrder();
            if (e.hasShootAtEntityOrder)     e.RemoveShootAtEntityOrder();
        }

        _newShootAtDirection.ClearCollectedEntities();
        _newShootAtPosition.ClearCollectedEntities();
        _newShootAtEntity.ClearCollectedEntities();
    }
}