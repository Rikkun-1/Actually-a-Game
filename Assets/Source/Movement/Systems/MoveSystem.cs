using Entitas;

public class MoveSystem : IExecuteSystem
{
    private readonly IGroup<GameEntity> _entities;

    public MoveSystem(Contexts contexts)
    {
        _entities = contexts.game.GetGroup(GameMatcher.AllOf(GameMatcher.WorldPosition,
                                                             GameMatcher.Velocity));
    }

    public void Execute()
    {
        foreach (var e in _entities)
        {
            e.ReplaceWorldPosition(e.worldPosition.value + e.velocity.value * GameTime.deltaTime);
        }
    }
}