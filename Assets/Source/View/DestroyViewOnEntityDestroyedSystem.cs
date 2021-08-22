using Entitas;

public class DestroyViewOnEntityDestroyedSystem : ICleanupSystem
{
    private readonly IGroup<GameEntity> _entities;

    public DestroyViewOnEntityDestroyedSystem(Contexts contexts)
    {
        _entities = contexts.game.GetGroup(GameMatcher.AllOf(GameMatcher.Destroyed,
                                                             GameMatcher.UnityView));
    }

    public void Cleanup()
    {
        foreach (var e in _entities)
        {
            UnityViewHelper.DestroyView(e);
        }
    }
}