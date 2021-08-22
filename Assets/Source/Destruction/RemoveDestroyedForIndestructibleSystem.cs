using Entitas;

public class RemoveDestroyedForIndestructibleSystem : ICleanupSystem
{
    private readonly IGroup<GameEntity> _entities;

    public RemoveDestroyedForIndestructibleSystem(Contexts contexts)
    {
        _entities =
            contexts.game.GetGroup(GameMatcher.AllOf(GameMatcher.Destroyed,
                                                     GameMatcher.Indestructible));
    }

    public void Cleanup()
    {
        foreach (var e in _entities.GetEntities())
        {
            e.isDestroyed = false;
        }
    }
}