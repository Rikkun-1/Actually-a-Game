using UnityEngine;

public class EntityCreator
{
    private        const long _startID = 0;
    private static       long _currentID;

    public static long currentID
    {
        get => _currentID;
    }

    public EntityCreator()
    {
        _currentID = _startID;
    }
    
    public static GameEntity CreateGameEntity()
    {
        var e = Contexts.sharedInstance.game.CreateEntity();
        e.AddID(_currentID++);

        return e;
    }
}
