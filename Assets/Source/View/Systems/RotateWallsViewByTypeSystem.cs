using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using Entitas;

public class RotateWallsViewByTypeSystem : ReactiveSystem<GameEntity>
{
    private readonly Contexts _contexts;

    public RotateWallsViewByTypeSystem(Contexts contexts) : base(contexts.game)
    {
        _contexts = contexts;
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.AnyOf(GameMatcher.NorthWall,
                                                         GameMatcher.SouthWall,
                                                         GameMatcher.WestWall,
                                                         GameMatcher.EastWall));
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.hasUnityView;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        foreach (var e in entities)
        {
            var angle = 0f;
            if (e.isNorthWall) angle      = 0f;
            else if (e.isEastWall) angle  = 90f;
            else if (e.isSouthWall) angle = 180f;
            else if (e.isWestWall) angle  = 270f;
            
            e.unityView.gameObject.transform.rotation = Quaternion.Euler(0, angle, 0);
        }
    }
}