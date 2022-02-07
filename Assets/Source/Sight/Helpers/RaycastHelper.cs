using Unity.Collections;
using UnityEngine;

public static class RaycastHelper
{
    private static readonly LayerMask _layerMask;

    private static NativeArray<RaycastCommand> _commands;
    private static NativeArray<RaycastHit>     _results;

    static RaycastHelper()
    {
        _layerMask.value = LayerMask.GetMask("Default");
    }

    public static bool IsInClearVision(Vector3 origin, Vector3 target)
    {
        var raycastDirection = target - origin;
        return Physics.Raycast(origin, raycastDirection, Vector3.Distance(origin, target), _layerMask) == false;
    }
    
    public static bool IsInClearVision(in Vector3 origin, GameEntity targetEntity)
    {
        if (!targetEntity.hasUnityView) return false;

        var targetPosition   = targetEntity.worldPosition.value.WithY(1.4f);
        var distance         = Vector3.Distance(origin, targetPosition);

        var raycastDirection = targetPosition - origin;

        return !Physics.Raycast(origin, raycastDirection, distance, _layerMask);
    }

    public static bool IsInClearVision(GameEntity e, GameEntity targetEntity)
    {
        var raycastOrigin = e.worldPosition.value.WithY(1.4f);

        return IsInClearVision(raycastOrigin, targetEntity);
    }

    public static NativeArray<RaycastHit> BatchedRaycast(Vector3[] from, Vector3[] to, LayerMask layerMask)
    {
        AllocateArrays(from.Length);
        SetupRaycastCommands(from, to, layerMask);
        RaycastCommand.ScheduleBatch(_commands, _results, 16).Complete();

        return _results;
    }

    private static void AllocateArrays(int size)
    {
        if (_commands.Length >= size) return;
        if (_commands.IsCreated) _commands.Dispose();
        if (_results.IsCreated)  _results.Dispose();

        _commands = new NativeArray<RaycastCommand>(size, Allocator.Persistent);
        _results  = new NativeArray<RaycastHit>(size, Allocator.Persistent);
    }

    private static void SetupRaycastCommands(Vector3[] from, Vector3[] to, LayerMask layerMask)
    {
        for (var i = 0; i < from.Length; i++)
        {
            var direction = to[i] - from[i];
    
            _commands[i] = new RaycastCommand(from[i], direction, direction.magnitude, layerMask);
        }
    }

    public static void DisposeNativeArrays()
    {
        if (_commands.IsCreated) _commands.Dispose();
        if (_results.IsCreated)  _results.Dispose();
    }
    
    public static bool Linecast(Vector3 start, Vector3 end, out RaycastHit raycastHit, int layerMask)
    {
        var direction = end - start;
        var distance  = Vector3.Distance(end, start);
        return Physics.Raycast(start, direction, out raycastHit, distance, layerMask);
    }
}