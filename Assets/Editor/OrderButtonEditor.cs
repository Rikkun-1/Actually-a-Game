using System;
using System.Linq;
using DesperateDevs.Utils;
using Entitas;
using UnityEditor;


[CustomEditor(typeof(OrderButton))]
public class OrderButtonEditor : Editor
{
    private OrderButton _orderButton;
    private int         _popupOrderIndex;
    private string[]    _possibleOrderNames;

    private void OnEnable()
    {
        _orderButton    = (OrderButton)target;
        
        _possibleOrderNames = GameComponentsLookup.componentTypes
                                                  .Where(type => type.ImplementsInterface<IOrderComponent>())
                                                  .Select(type => type.Name.RemoveComponentSuffix())
                                                  .ToArray();

        _popupOrderIndex = Array.IndexOf(_possibleOrderNames, _orderButton.orderName);
    }

    public override void OnInspectorGUI()
    {
        EditorGUI.BeginChangeCheck();

        DrawDefaultInspector();

        _popupOrderIndex = EditorGUILayout.Popup(_popupOrderIndex, _possibleOrderNames);

        if (EditorGUI.EndChangeCheck())
        {
            _orderButton.orderName = _possibleOrderNames[_popupOrderIndex];
            EditorUtility.SetDirty(target);
            
            Repaint();
        }
    }
}