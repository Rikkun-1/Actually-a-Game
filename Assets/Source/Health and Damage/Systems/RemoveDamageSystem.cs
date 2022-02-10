using Entitas;

public class RemoveDamageSystem : ICleanupSystem
{
    private readonly IGroup<GameEntity> _entities;

    public RemoveDamageSystem(Contexts contexts)
    {
        _entities = contexts.game.GetGroup(GameMatcher.Damage);
    }

    public void Cleanup()
    {
        foreach (var e in _entities.GetEntities())
        {
            e.RemoveDamage();
        }
    }
}