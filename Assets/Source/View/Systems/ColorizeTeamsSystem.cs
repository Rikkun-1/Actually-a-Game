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
        return context.CreateCollector(GameMatcher.TeamColor);
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.hasUnityView && entity.hasTeamColor;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        foreach (var e in entities)
        {
            var color = e.teamColor.value;
            Color.RGBToHSV(color, out var h, out _, out _);
            
            foreach (var renderer in e.unityView.gameObject.GetComponentsInChildren<SkinnedMeshRenderer>())
            {
                renderer.material.color = color;
            }

            var colorTheme = e.unityView.gameObject.GetComponentInChildren<ColorTheme>();
            
            Color.RGBToHSV(colorTheme.background, out _, out var s, out var v);
            colorTheme.background = Color.HSVToRGB(h, s, v);
            
            Color.RGBToHSV(colorTheme.foreground, out _, out s, out v);
            colorTheme.foreground = Color.HSVToRGB(h, s, v);
            colorTheme.UpdateChildren();
        }
    }
}