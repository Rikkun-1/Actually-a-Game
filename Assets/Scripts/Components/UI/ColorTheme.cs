using UnityEngine;

public class ColorTheme : MonoBehaviour
{
    public Color background;
    public Color foreground;
    
    public enum ThemeColor
    {
        Background,
        Foreground
    }
    
    [ContextMenu("Update children")]
    public void UpdateChildren()
    {
        foreach (var getColorFromColorTheme in GetComponentsInChildren<GetColorFromColorTheme>())
        {
            getColorFromColorTheme.Start();
        }
    }
}
