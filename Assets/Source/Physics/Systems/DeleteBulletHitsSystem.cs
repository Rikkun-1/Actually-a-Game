using Entitas;

public class DeleteBulletHitsSystem : ICleanupSystem
{
    private readonly IGroup<PhysicsEntity> _entities;

    public DeleteBulletHitsSystem(Contexts contexts)
    {
        _entities = contexts.physics.GetGroup(PhysicsMatcher.BulletHit);
    }

    public void Cleanup()
    {
        foreach (var e in _entities)
        {
            e.isDeleted = true;
        }
    }
}