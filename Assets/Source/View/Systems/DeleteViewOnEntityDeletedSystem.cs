using Entitas;

public class DeleteViewOnEntityDeletedSystem : ICleanupSystem
{
    private readonly IGroup<GameEntity> _entities;

    public DeleteViewOnEntityDeletedSystem(Contexts contexts)
    {
        _entities = contexts.game.GetGroup(GameMatcher.AllOf(GameMatcher.Deleted,
                                                             GameMatcher.UnityView)
                                                      .NoneOf(GameMatcher.Indestructible));
    }

    public void Cleanup()
    {
        foreach (var e in _entities.GetEntities())
        {
            UnityViewHelper.DestroyView(e);
        }
    }
}