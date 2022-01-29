using Entitas;

public class RemoveDestroyedForIndestructibleSystem : ICleanupSystem
{
    private readonly IGroup<GameEntity>    _gameEntities;
    private readonly IGroup<PhysicsEntity> _physicalEntities;

    public RemoveDestroyedForIndestructibleSystem(Contexts contexts)
    {
        _gameEntities = contexts.game.GetGroup(GameMatcher.AllOf(GameMatcher.Destroyed,
                                                                 GameMatcher.Indestructible));
        _physicalEntities = contexts.physics.GetGroup(PhysicsMatcher.AllOf(PhysicsMatcher.Destroyed,
                                                                           PhysicsMatcher.Indestructible));
    }

    public void Cleanup()
    {
        foreach (var e in _gameEntities.GetEntities()) e.isDestroyed = false;
        foreach (var e in _physicalEntities.GetEntities()) e.isDestroyed = false;
    }
}