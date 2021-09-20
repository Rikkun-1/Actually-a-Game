public static class EntityCreator
{
    public static long currentID { get; private set; }

    public static GameEntity CreateGameEntity()
    {
        var e = Contexts.sharedInstance.game.CreateEntity();
        e.AddId(currentID++);

        return e;
    }
}