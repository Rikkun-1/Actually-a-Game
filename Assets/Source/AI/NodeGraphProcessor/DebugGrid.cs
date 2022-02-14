using UnityEngine;

public static class DebugGrid
{
    private static WorldDebugGrid _worldDebugGrid;

    public static void SetValues(int[,] newValues)
    {
        var height = newValues.GetLength(0);
        var width  = newValues.GetLength(1);

        _worldDebugGrid?.Destroy();
        _worldDebugGrid = new WorldDebugGrid(width, height, 1, new Vector3());
        _worldDebugGrid.SetValues(newValues);
    }
}