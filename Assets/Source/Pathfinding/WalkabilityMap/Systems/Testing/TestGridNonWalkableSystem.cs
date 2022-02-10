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
        var gridSize = _game.gridSize.value;

        var x = Random.Range(0, gridSize.x);
        var z = Random.Range(0, gridSize.y);

        var e = EntityCreator.CreateGameEntity();
        e.AddWorldPosition(new Vector3(x, 0, z));
        e.isNonWalkable = true;
        e.AddViewPrefab("nonWalkable");

        if (_nonWalkableEntities.count >= gridSize.x * gridSize.y / 5)
        {
            var rand = Random.Range(0, _nonWalkableEntities.count);

            _nonWalkableEntities.GetEntities()[rand].isDestroyed = true;
        }
    }
}