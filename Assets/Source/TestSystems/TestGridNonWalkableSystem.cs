using Entitas;

public class TestGridNonWalkableSystem : IExecuteSystem
{
    readonly IGroup<GameEntity> entities;

    public TestGridNonWalkableSystem(Contexts contexts)
    {
        this.entities = contexts.game.GetGroup(GameMatcher.AllOf(GameMatcher.Position)
                                                          .NoneOf(GameMatcher.NorthWall, 
                                                                  GameMatcher.SouthWall, 
                                                                  GameMatcher.WestWall,
                                                                  GameMatcher.EastWall));
    }

    public void Execute()
    {
        var rand = UnityEngine.Random.Range(0, this.entities.count);
        var e = this.entities.GetEntities()[rand];

        e.isNonWalkable = UnityEngine.Random.Range(0, 2) == 0;
        e.ReplaceViewPrefab(e.isNonWalkable ? "nonWalkable" : "floor");
    }
}