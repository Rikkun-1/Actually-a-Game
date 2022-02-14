using Entitas;

public class DeletionSystem : ICleanupSystem
{
    private readonly IGroup<GameEntity>    _gameEntities;
    private readonly IGroup<PhysicsEntity> _physicalEntities;

    public DeletionSystem(Contexts contexts)
    {
        _gameEntities     = contexts.game.GetGroup(GameMatcher.Deleted);
        _physicalEntities = contexts.physics.GetGroup(PhysicsMatcher.Deleted);
    }

    public void Cleanup()
    {
        foreach (var e in _gameEntities.GetEntities()) e.Destroy();
        foreach (var e in _physicalEntities.GetEntities()) e.Destroy();
    }
}