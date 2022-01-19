public static class EntityCreator
{
    public static long currentID { get; private set; }

    public static GameEntity CreateGameEntity(Contexts contexts)
    {
        var e = contexts.game.CreateEntity();
        e.AddId(currentID++);

        return e;
    }

    public static GameEntity CreateGameEntity()
    {
        return CreateGameEntity(Contexts.sharedInstance);
    }
}