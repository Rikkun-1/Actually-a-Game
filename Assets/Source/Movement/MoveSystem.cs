using Entitas;

public class MoveSystem : IExecuteSystem
{
    private readonly Contexts           _contexts;
    private readonly IGroup<GameEntity> _entities;

    public MoveSystem(Contexts contexts)
    {
        _contexts = contexts;
        _entities = contexts.game.GetGroup(GameMatcher.AllOf(GameMatcher.WorldPosition,
                                                             GameMatcher.Velocity));
    }

    public void Execute()
    {
        var deltaTime = _contexts.game.simulationTick.deltaTime;

        foreach (var e in _entities)
        {
            e.ReplaceWorldPosition(e.worldPosition.value + e.velocity.value * deltaTime);
        }
    }
}