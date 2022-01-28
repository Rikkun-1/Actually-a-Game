using Entitas;

public class DeleteCollisionsSystem : ICleanupSystem
{
    private readonly IGroup<GameEntity> _entities;

    public DeleteCollisionsSystem(Contexts contexts)
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