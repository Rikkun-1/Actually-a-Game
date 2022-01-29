using Entitas;

public class DeleteBulletHitsSystem : ICleanupSystem
{
    private readonly IGroup<GameEntity> _entities;

    public DeleteBulletHitsSystem(Contexts contexts)
    {
        _entities = contexts.game.GetGroup(GameMatcher.BulletHit);
    }

    public void Cleanup()
    {
        foreach (var e in _entities)
        {
            e.isDestroyed = true;
        }
    }
}