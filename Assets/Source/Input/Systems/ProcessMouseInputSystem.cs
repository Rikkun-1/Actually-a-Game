using Entitas;
using UnityEngine;
using UnityEngine.EventSystems;

public class ProcessMouseInputSystem : IInitializeSystem, IExecuteSystem
{
    private readonly InputContext _input;
    private readonly LayerMask    _layerMask;

    private Camera _mainCamera;

    public ProcessMouseInputSystem(Contexts contexts)
    {
        _layerMask.value = LayerMask.GetMask("Ground");
        _input           = contexts.input;
    }

    public void Initialize()
    {
        _mainCamera = Camera.main;
    }

    public void Execute()
    {
        ProcessMove();
        ProcessClick();
    }

    private void ProcessMove()
    {
        if (IsOverUI()) return;
        
        var ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out var raycastHit, 1000f, _layerMask))
        {
            _input.ReplaceMousePosition(raycastHit.point.ToVector2XZInt());
        }
    }

    private void ProcessClick()
    {
        if (!Input.GetMouseButtonDown(0) || IsOverUI()) return;

        var ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray , out var raycastHit, 1000f, _layerMask))
        {
            _input.ReplaceMouseGridClickPosition(raycastHit.point.ToVector2XZInt());
        }
    }

    private static bool IsOverUI()
    {
        return EventSystem.current.IsPointerOverGameObject();
    }
}