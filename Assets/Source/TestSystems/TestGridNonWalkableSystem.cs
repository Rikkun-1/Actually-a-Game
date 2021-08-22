using System.Linq;
using Entitas;
using UnityEngine;

public class TestGridNonWalkableSystem : IExecuteSystem
{
    private readonly GameContext        _game;
    private readonly IGroup<GameEntity> _nonWalkableEntities;

    public TestGridNonWalkableSystem(Contexts contexts)
    {
        _game                = contexts.game;
        _nonWalkableEntities = contexts.game.GetGroup(GameMatcher.NonWalkable);
    }

    public void Execute()
    {
        var mapSize = _game.GetEntities(GameMatcher.MapSize).ToList().SingleEntity().mapSize.value;

        var x = Random.Range(0, mapSize.x);
        var y = Random.Range(0, mapSize.y);

        var e = _game.CreateEntity();
        e.AddPosition(new Vector2Int(x, y));
        e.isNonWalkable = true;
        e.AddViewPrefab("nonWalkable");

        if (_nonWalkableEntities.count >= mapSize.x * mapSize.y / 5)
        {
            var rand = Random.Range(0, _nonWalkableEntities.count);

            _nonWalkableEntities.GetEntities()[rand].isDestroyed = true;
        }
    }
}