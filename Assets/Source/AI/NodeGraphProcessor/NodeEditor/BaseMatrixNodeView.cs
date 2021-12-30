using System;
using GraphProcessor;
using UnityEngine;
using UnityEngine.UIElements;
using Object = UnityEngine.Object;

[NodeCustomEditor(typeof(BaseMatrixNode))]
public class BaseMatrixNodeView : BaseNodeView
{
    private   Image          _preview;
    private   Label          _processingTimeLabel;
    protected bool           normalize;
    protected BaseMatrixNode target;

    public override void Enable()
    {
        base.Enable();
        SetupOnProcessedCallback();
        SetupPreview();
        SetupTitleButton("Show in game", UpdateDebugGrid);
        SetupProcessingTimeLabel("Processing Time: ");
        SetupNormalizeToggle();
    }

    public override void Disable()
    {
        Object.DestroyImmediate(_preview?.image);
    }

    private void SetupPreview()
    {
        if (_preview != null) return;

        _preview = new Image();

        var nodeWidth = target.position.width;
        var size      = nodeWidth > 250 ? nodeWidth : 250;

        _preview.style.height = size;
        _preview.style.width  = size;

        contentContainer.Add(_preview);
    }

    private void SetupOnProcessedCallback()
    {
        target = (BaseMatrixNode)nodeTarget;
        target.onProcessed += () =>
        {
            UpdatePreviewImage(_preview, target?.output);
            UpdateProcessingTime();
        };
    }

    private void SetupNormalizeToggle()
    {
        var toggleNormalize = new Toggle("Normalize");
        toggleNormalize.RegisterValueChangedCallback(value => normalize = value.newValue);
        controlsContainer.Add(toggleNormalize);
    }

    private void SetupProcessingTimeLabel(string processingTimeLabelText)
    {
        _processingTimeLabel = new Label(processingTimeLabelText);
        contentContainer.Add(_processingTimeLabel);
    }

    private void SetupTitleButton(string text, Action clickEvent)
    {
        var titleButton = new Button(clickEvent);
        titleButton.text = text;
        titleButtonContainer.Add(titleButton);
    }

    private void UpdateDebugGrid()
    {
        if (target?.output is null) return;

        DebugGrid.SetValues(target.output.ToIntArray());
    }

    private void UpdateProcessingTime()
    {
        _processingTimeLabel.text = "Processing Time: " + target.processingTime + " ms";
    }

    private void UpdatePreviewImage(Image image, Matrix matrix)
    {
        if (matrix is null) return;

        image.scaleMode = ScaleMode.ScaleToFit;
        Object.DestroyImmediate(image.image);
        image.image = CreateTextureFromMatrix(matrix);
    }

    protected Texture2D CreateTextureFromMatrix(Matrix matrix)
    {
        var texture = new Texture2D(matrix.width, matrix.height);

        FillPreviewTexture(matrix, texture);

        texture.filterMode = FilterMode.Point;
        texture.Apply();
        return texture;
    }

    protected virtual void FillPreviewTexture(Matrix matrix, Texture2D texture)
    {
        var divider = GetColorDivider();

        for (var x = 0; x < matrix.width; x++)
        {
            for (var y = 0; y < matrix.height; y++)
            {
                texture.SetPixel(x, y, new Color(0, matrix[x, y] / divider, 0).gamma);
            }
        }
    }

    protected virtual float GetColorDivider()
    {
        return normalize
                   ? target.output.Max().value
                   : 100f;
    }
}