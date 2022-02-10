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
        HandleNewShootAtEntityOrder();
        HandleNewShootAtPositionOrder();
        HandleNewShootAtDirectionOrder();
        ClearCollectedEntities();
    }

    private void HandleNewShootAtEntityOrder()
    {
        foreach (var e in _newShootAtEntity.collectedEntities)
        {
            if (!e.hasShootAtEntityOrder)   continue;
            if (e.hasShootAtDirectionOrder) e.RemoveShootAtDirectionOrder();
            if (e.hasShootAtPositionOrder)  e.RemoveShootAtPositionOrder();
        }
    }

    private void HandleNewShootAtPositionOrder()
    {
        foreach (var e in _newShootAtPosition.collectedEntities)
        {
            if (!e.hasShootAtPositionOrder) continue;
            if (e.hasShootAtDirectionOrder) e.RemoveShootAtDirectionOrder();
            if (e.hasShootAtEntityOrder)    e.RemoveShootAtEntityOrder();
        }
    }

    private void HandleNewShootAtDirectionOrder()
    {
        foreach (var e in _newShootAtDirection.collectedEntities)
        {
            if (!e.hasShootAtDirectionOrder) continue;
            if (e.hasShootAtPositionOrder)   e.RemoveShootAtPositionOrder();
            if (e.hasShootAtEntityOrder)     e.RemoveShootAtEntityOrder();
        }
    }

    private void ClearCollectedEntities()
    {
        _newShootAtDirection.ClearCollectedEntities();
        _newShootAtPosition.ClearCollectedEntities();
        _newShootAtEntity.ClearCollectedEntities();
    }
}