using Entitas;

public class DestroySystem : ICleanupSystem
{
    readonly Contexts contexts;
    readonly IGroup<GameEntity> entities;

    public DestroySystem(Contexts contexts)
    {
        this.contexts = contexts;
        this.entities = this.contexts.game.GetGroup(GameMatcher.Destroyed);
    }

    public void Cleanup()
    {
        foreach (var e in this.entities.GetEntities())
        {
            e.Destroy();
        }
    }
}