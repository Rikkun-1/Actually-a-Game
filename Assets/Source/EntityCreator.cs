using Entitas;

public class EntityCreator
{
    private static long _currentID = 0;

    public static GameEntity createGameEntity()
    {
        var e = Contexts.sharedInstance.game.CreateEntity();
        e.AddID(_currentID++);

        return e;
    }
}
