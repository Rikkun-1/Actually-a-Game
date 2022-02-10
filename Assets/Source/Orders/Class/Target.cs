using System;
using UnityEngine;

public class Target
{
    public TargetType targetType;
    
    public Target()
    {
        targetType = TargetType.Direction;
        _direction = Vector2.one;
    }

    public Vector2    direction => _direction ?? throw new NullReferenceException("direction not set");
    public Vector2Int position  => _position ?? throw new NullReferenceException("position not set");
    public long       entityID  => _entityID ?? throw new NullReferenceException("entityID not set");

    private Vector2?    _direction;
    private Vector2Int? _position;
    private long?       _entityID;

    public static Target Direction(Vector2 direction)
    {
        return new Target {targetType = TargetType.Direction, _direction = direction};
    }
    
    public static Target Position(Vector2Int position)
    {
        return new Target {targetType = TargetType.Position, _position = position};
    }
    
    public static Target Entity(long entityID)
    {
        return new Target {targetType = TargetType.Entity, _entityID = entityID};
    }
}