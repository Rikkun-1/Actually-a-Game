using System.Collections.Generic;
using Entitas;
using UnityEngine;

public class ColorizeTeamsSystem : ReactiveSystem<GameEntity>
{
    public ColorizeTeamsSystem(Contexts contexts) : base(contexts.game)
    {
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.TeamID);
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.hasUnityView;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        foreach (var e in entities)
        {
            if (e.teamID.value == 1)
            {
                foreach (var renderer in e.unityView.gameObject.GetComponentsInChildren<SkinnedMeshRenderer>())
                {
                    renderer.material.color = Color.red;
                }
            }
        }
    }
}