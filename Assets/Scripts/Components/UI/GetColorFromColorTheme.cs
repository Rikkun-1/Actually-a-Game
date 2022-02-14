using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
internal class GetColorFromColorTheme : MonoBehaviour
{
    public ColorTheme.ThemeColor themeColor;

    public void Start()
    {
        var theme = GetComponentInParent<ColorTheme>();
        var image = GetComponent<Image>();

        image.color = themeColor switch
        {
            ColorTheme.ThemeColor.Background => theme.background, 
            ColorTheme.ThemeColor.Foreground => theme.foreground, 
            _ => throw new ArgumentOutOfRangeException()
        };
    }
}