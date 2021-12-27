/* 
    ------------------- Code Monkey -------------------

    Thank you for downloading this package
    I hope you find it useful in your projects
    If you have any questions let me know
    Cheers!

               unitycodemonkey.com
    --------------------------------------------------
 */

using System;
using CodeMonkey.Utils;
using UnityEngine;
using Object = UnityEngine.Object;

public class WorldDebugGrid
{
    public event EventHandler<OnGridValueChangedEventArgs> OnGridValueChanged;

    public class OnGridValueChangedEventArgs : EventArgs
    {
        public int x;
        public int y;
    }

    private readonly GameObject  _parent;
    public readonly  int         width;
    public readonly  int         height;
    public readonly  float       cellSize;
    private readonly Vector3     _originPosition;
    private readonly int[,]      _gridArray;
    private readonly TextMesh[,] _textMeshes;

    public WorldDebugGrid(int width, int height, float cellSize, Vector3 originPosition)
    {
        this.width      = width;
        this.height     = height;
        this.cellSize   = cellSize;
        _originPosition = originPosition;

        _gridArray = new int[width, height];

        _textMeshes                = new TextMesh[width, height];
        _parent                    = new GameObject("debug grid");
        _parent.transform.rotation = Quaternion.Euler(90f, 0f, 0f);

        for (var x = 0; x < _gridArray.GetLength(0); x++)
        {
            for (var y = 0; y < _gridArray.GetLength(1); y++)
            {
                _textMeshes[x, y] =
                    UtilsClass.CreateWorldText(_gridArray[x, y].ToString(), _parent.transform, GetWorldPosition(x, y), 30, Color.black, TextAnchor.MiddleCenter);
            }
        }

        OnGridValueChanged += (object sender, OnGridValueChangedEventArgs eventArgs) =>
        {
            _textMeshes[eventArgs.x, eventArgs.y].text = _gridArray[eventArgs.x, eventArgs.y].ToString();
        };
    }

    public void Destroy()
    {
        foreach (var textMesh in _textMeshes)
        {
            Object.Destroy(textMesh.gameObject);
        }

        Object.Destroy(_parent);
    }

    public void Show()
    {
        foreach (var textMesh in _textMeshes)
        {
            textMesh.gameObject.SetActive(true);
        }
    }

    public void Hide()
    {
        foreach (var textMesh in _textMeshes)
        {
            textMesh.gameObject.SetActive(false);
        }
    }

    public Vector3 GetWorldPosition(int x, int y)
    {
        return new Vector3(x, y) * cellSize + _originPosition;
    }

    private void GetXY(Vector3 worldPosition, out int x, out int y)
    {
        x = Mathf.FloorToInt((worldPosition - _originPosition).x / cellSize);
        y = Mathf.FloorToInt((worldPosition - _originPosition).y / cellSize);
    }

    public void SetValues(int[,] values)
    {
        for (var i = 0; i < values.GetLength(0); i++)
        {
            for (var j = 0; j < values.GetLength(1); j++)
            {
                SetValue(i, j, values[i, j]);
            }
        }
    }

    public void SetValue(int x, int y, int value)
    {
        if (x >= 0 && y >= 0 && x < width && y < height)
        {
            _gridArray[x, y] = value;
            if (OnGridValueChanged != null) OnGridValueChanged(this, new OnGridValueChangedEventArgs { x = x, y = y });

            _textMeshes[x, y].color = value == 0
                                          ? Color.black
                                          : new Color(2.0f * (1 - value / 100f), 2.0f * (1 - (1 - value / 100f)), 0);
        }
    }

    public void SetValue(Vector3 worldPosition, int value)
    {
        int x, y;
        GetXY(worldPosition, out x, out y);
        SetValue(x, y, value);
    }

    public int GetValue(int x, int y)
    {
        if (x >= 0 && y >= 0 && x < width && y < height)
        {
            return _gridArray[x, y];
        }

        return 0;
    }

    public int GetValue(Vector3 worldPosition)
    {
        int x, y;
        GetXY(worldPosition, out x, out y);
        return GetValue(x, y);
    }
}