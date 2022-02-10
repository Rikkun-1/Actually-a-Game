using Entitas;

public class DeleteCollisionSystem : ICleanupSystem
{
    private readonly IGroup<GameEntity> _entities;

    public DeleteCollisionSystem(Contexts contexts)
    {
        _entities = contexts.game.GetGroup(GameMatcher.Collision);
    }

    public void Cleanup()
    {
        foreach (var e in _entities)
        {
            e.isDestroyed = true;
        }
    }
}