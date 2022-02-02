using Entitas;

public class RemoveDestroyedForIndestructibleSystem : ICleanupSystem
{
    private readonly IGroup<GameEntity>    _gameEntities;

    public RemoveDestroyedForIndestructibleSystem(Contexts contexts)
    {
        _gameEntities = contexts.game.GetGroup(GameMatcher.AllOf(GameMatcher.Destroyed,
                                                                 GameMatcher.Indestructible));
    }

    public void Cleanup()
    {
        foreach (var e in _gameEntities.GetEntities())
        {
            e.isDestroyed = false;
        }
    }
}