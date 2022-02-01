public static class GameEntityCreator
{
    public static GameEntity CreateEntity(Contexts contexts)
    {
        var e = contexts.game.CreateEntity();
        e.AddId(e.creationIndex);

        return e;
    }

    public static GameEntity CreateEntity()
    {
        return CreateEntity(Contexts.sharedInstance);
    }
}