using System;
using GraphProcessor;
using UnityEngine;

public static class DebugGrid
{
    // private static Grid          _worldDebugGrid;
    
    private static WorldDebugGrid _worldDebugGrid;

    public static void SetValues(int[,] newValues)
    {
        var height = newValues.GetLength(0);
        var width = newValues.GetLength(1);

        // _worldDebugGrid = new Grid(width, height, 1, new Vector3());
        //
        // var gameObject = new GameObject("Heat Map Visual", new []
        // {
        //     typeof(MeshFilter),
        //     typeof(MeshRenderer),
        //     typeof(HeatMapVisual)
        // });
        // gameObject.GetComponent<HeatMapVisual>().SetGrid(_worldDebugGrid);
        //
        // _worldDebugGrid.SetValues(newValues);
        
        _worldDebugGrid?.Destroy(); 
        _worldDebugGrid = new WorldDebugGrid(width, height, 1, new Vector3());
        _worldDebugGrid.SetValues(newValues);
    }
}

public class Test : MonoBehaviour
{
    public BaseGraph             graph;
    public ProcessGraphProcessor processor;
    public int[,]                outMatrix;

    public void Update()
    {
        processor ??= new ProcessGraphProcessor(graph);

        var h = 10;
        var k = 10;
        var r = 5;

        var inputMatrix1 = new Matrix(20, 20);
        inputMatrix1 = inputMatrix1.ForEach((x, y, _) => Math.Abs(Mathf.Pow(x - h, 2) + (Mathf.Pow(y - k, 2)) - r * r) < 5f ? 100 : 0);

        var inputMatrix2 = new Matrix(20, 20);
        inputMatrix2 = inputMatrix2.ForEach((x, y, _) => x == h || y == k ? 100 : 0);

        var inputMatrix3 = new Matrix(20, 20);
        for (int i = 0; i < 20; i++)
        {
            inputMatrix3[i, 0] = 100;
            inputMatrix3[i, i] = i * 5;
        }

        graph.SetParameterValue("Input1", inputMatrix1);
        graph.SetParameterValue("Input2", inputMatrix2);
        graph.SetParameterValue("Input3", inputMatrix3);
        processor.Run();
        
        outMatrix = inputMatrix1.ToIntArray();
    }
}