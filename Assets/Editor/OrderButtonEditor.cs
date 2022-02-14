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
        var orderName = HandleOrderName();
        HandleLookShootOrders(orderName);
    }

    private void HandleLookShootOrders(string orderName)
    {
        if (orderName != "LookOrder" && orderName != "ShootOrder") return;
        
        EditorGUI.BeginChangeCheck();

        Enum.TryParse<TargetType>(_orderButton.orderArgument, out var targetType);
        var newTargetType = (TargetType)EditorGUILayout.EnumPopup(targetType);

        if (EditorGUI.EndChangeCheck())
        {
            _orderButton.orderArgument = newTargetType.ToString();
            EditorUtility.SetDirty(target);
            Repaint();
        }
    }

    private string HandleOrderName()
    {
        EditorGUI.BeginChangeCheck();

        DrawDefaultInspector();

        _popupOrderIndex = EditorGUILayout.Popup(_popupOrderIndex, _possibleOrderNames);

        var orderName = _possibleOrderNames[_popupOrderIndex];

        if (EditorGUI.EndChangeCheck())
        {
            _orderButton.orderName = orderName;
            EditorUtility.SetDirty(target);
            Repaint();
        }

        return orderName;
    }
}