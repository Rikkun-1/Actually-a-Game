using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
class ButtonShortcut : MonoBehaviour
{
    public KeyCode         key;
    public TextMeshProUGUI shortcutText;

    private Button _button;
    
    private void Start()
    {
        _button = GetComponent<Button>();
        SetShortcutText();
    }

    private void Update()
    {
        if (!Input.GetKeyDown(key)) return;
        
        _button.Select();
        _button.onClick.Invoke();
    }

    [ContextMenu("Set Shortcut Text")]
    public void SetShortcutText()
    {
        var text = key.ToString();
        text = RemoveNumberPrefix(text);

        shortcutText.text = text;
    }

    private static string RemoveNumberPrefix(string text)
    {
        if (text.StartsWith("Alpha") || text.StartsWith("Keypad"))
        {
            text = new string(text.Where(char.IsDigit).ToArray());
        }

        return text;
    }
}