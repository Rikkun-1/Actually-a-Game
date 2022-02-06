using UnityEngine;
using UnityEngine.Animations;

public static class OrderVisualizationHelper
{
    public static void PaintInTeamColor(GameObject visualizationInstance, GameEntity selectedGameEntity)
    {
        if (!selectedGameEntity.hasTeamColor) return;
        
        var color = GetColor(selectedGameEntity);

        PaintSpriteRenderer(visualizationInstance, color);
        PaintLineRenderer(visualizationInstance, color);
    }

    public static void PaintLineRenderer(GameObject visualizationInstance, Color color)
    {
        var lineRenderer = visualizationInstance.GetComponent<LineRenderer>();
        if (lineRenderer == null) return;

        lineRenderer.startColor = color;
        lineRenderer.endColor   = color;
    }
    
    public static void PaintSpriteRenderer(GameObject visualizationInstance, Color color)
    {
        var spriteRenderer                       = visualizationInstance.GetComponent<SpriteRenderer>();
        if (spriteRenderer) spriteRenderer.color = color;
    }

    private static Color GetColor(GameEntity selectedGameEntity)
    {
        Color.RGBToHSV(selectedGameEntity.teamColor.value, out var h, out var s, out var v);
        return Color.HSVToRGB(h, s, v - 35 / 255f);
    }
    
    public static void SetupPositionConstraint(GameObject visualizationInstance, Transform viewTransform, Vector3 offset = new Vector3())
    {
        var positionConstraint = visualizationInstance.GetComponent<PositionConstraint>();
        positionConstraint.AddSource(new ConstraintSource { sourceTransform = viewTransform, weight = 1 });
        positionConstraint.translationOffset = offset;
    }
    
    public static void SetupAimConstraint(GameObject visualizationInstance, Transform viewTransform)
    {
        visualizationInstance.GetComponent<AimConstraint>()
                             .AddSource(new ConstraintSource { sourceTransform = viewTransform, weight = 1 });
    }
}
