using Entitas;

public class UpdatePreviousPositionSystem : IExecuteSystem
{
    private readonly IGroup<GameEntity> _entities;

    public UpdatePreviousPositionSystem(Contexts contexts)
    {
        _entities = contexts.game.GetGroup(GameMatcher.AllOf(GameMatcher.WorldPosition,
                                                             GameMatcher.PreviousWorldPositionMemorization));
    }

    public void Execute()
    {
        foreach (var e in _entities)
        {
            e.ReplacePreviousWorldPosition(e.worldPosition.value);
        }
    }
}