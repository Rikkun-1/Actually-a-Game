using Entitas;

public class MoveSystem : IExecuteSystem
{
    private readonly IGroup<GameEntity> _entities;
    private readonly GameContext        _game;

    public MoveSystem(Contexts contexts)
    {
        _game = contexts.game;
        _entities = contexts.game.GetGroup(GameMatcher.AllOf(GameMatcher.WorldPosition,
                                                             GameMatcher.Velocity));
    }

    public void Execute()
    {
        var deltaTime = _game.simulationTick.deltaTime;

        foreach (var e in _entities)
        {
            e.ReplaceWorldPosition(e.worldPosition.value + e.velocity.value * deltaTime);
        }
    }
}