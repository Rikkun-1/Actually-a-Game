public static class EntityCreator
{
    public static GameEntity CreateGameEntity(Contexts contexts)
    {
        var e = contexts.game.CreateEntity();
        e.AddId(e.creationIndex);

        return e;
    }

    public static GameEntity CreateGameEntity()
    {
        return CreateGameEntity(Contexts.sharedInstance);
    }
}