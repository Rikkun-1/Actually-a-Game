using System;
using System.Collections.Generic;
using Entitas;
using UnityEngine;

public class RotateWallsViewByTypeSystem : ReactiveSystem<GameEntity>
{
    public RotateWallsViewByTypeSystem(Contexts contexts) : base(contexts.game)
    {
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.Wall);
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.hasUnityView;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        foreach (var e in entities)
        {
            var angle = e.wall.direction switch
            {
                Direction.Top    => 0f,
                Direction.Right  => 90f,
                Direction.Bottom => 180f,
                Direction.Left   => 270f,
                _                => throw new ArgumentOutOfRangeException()
            };

            e.unityView.gameObject.transform.rotation = Quaternion.Euler(0, angle, 0);
        }
    }
}