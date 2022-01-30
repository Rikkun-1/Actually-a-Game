using Entitas;
using UnityEngine;

public class GetGridClickSystem : IInitializeSystem, IExecuteSystem
{
    private readonly InputContext _input;
    private readonly LayerMask    _layerMask;

    private                 Camera    _mainCamera;

    public GetGridClickSystem(Contexts contexts)
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
        if (Input.GetMouseButtonDown(0))
        {
            var ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out var hit, 1000f, _layerMask))
            {
                _input.ReplaceMouseGridClickPosition(hit.point.ToVector2XZInt());
            }
        }
    }
}