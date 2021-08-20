using Entitas;

public class DeletePathFindingComponentsOnEntityDestroyedSystem : IExecuteSystem
{
    private readonly IGroup<GameEntity> _entities;

    public DeletePathFindingComponentsOnEntityDestroyedSystem(Contexts contexts)
    {
        _entities = contexts.game.GetGroup(GameMatcher.AllOf(GameMatcher.Destroyed)
                                                      .AnyOf(GameMatcher.NonWalkable,
                                                             GameMatcher.NorthWall,
                                                             GameMatcher.SouthWall,
                                                             GameMatcher.WestWall,
                                                             GameMatcher.EastWall));
    }

    public void Execute()
    {
        foreach (var e in _entities.GetEntities())
        {
            e.isNonWalkable = false;
            e.isNorthWall   = false;
            e.isSouthWall   = false;
            e.isWestWall    = false;
            e.isEastWall    = false;
        }
    }
}