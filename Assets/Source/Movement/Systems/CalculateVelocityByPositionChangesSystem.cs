using Entitas;

public class CalculateVelocityByPositionChangesSystem : IExecuteSystem
{
    private readonly IGroup<GameEntity> _entities;

    public CalculateVelocityByPositionChangesSystem(Contexts contexts)
    {
        _entities = contexts.game.GetGroup(GameMatcher.AllOf(GameMatcher.CalculateVelocityByPositionChanges,
                                                             GameMatcher.PreviousWorldPosition,
                                                             GameMatcher.WorldPosition));
    }

    public void Execute()
    {
        foreach (var e in _entities)
        {
            var positionDifference = e.worldPosition.value - e.previousWorldPosition.value;
            e.ReplaceVelocity(positionDifference * (1f / GameTime.deltaTime));
            e.ReplacePreviousWorldPosition(e.worldPosition.value);
        }
    }
}