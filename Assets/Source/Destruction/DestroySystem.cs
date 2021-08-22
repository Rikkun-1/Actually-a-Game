using Entitas;

public class DestroySystem : ICleanupSystem
{
    private readonly IGroup<GameEntity> _entities;

    public DestroySystem(Contexts contexts)
    {
        _entities = contexts.game.GetGroup(GameMatcher.Destroyed);
    }

    public void Cleanup()
    {
        foreach (var e in _entities.GetEntities())
        {
            e.Destroy();
        }
    }
}