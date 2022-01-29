using Entitas;

public class DestroySystem : ICleanupSystem
{
    private readonly IGroup<GameEntity> _gameEntities;
    private readonly IGroup<PhysicsEntity> _physicalEntities;

    public DestroySystem(Contexts contexts)
    {
        _gameEntities = contexts.game.GetGroup(GameMatcher.AllOf(GameMatcher.Destroyed)
                                                          .NoneOf(GameMatcher.Indestructible));
        _physicalEntities = contexts.physics.GetGroup(PhysicsMatcher.AllOf(PhysicsMatcher.Destroyed)
                                                                    .NoneOf(PhysicsMatcher.Indestructible));
    }

    public void Cleanup()
    {
        foreach (var e in _gameEntities.GetEntities()) e.Destroy();
        foreach (var e in _physicalEntities.GetEntities()) e.Destroy();
    }
}