using GraphProcessor;
using UnityEngine;
using UnityEngine.UIElements;

[NodeCustomEditor(typeof(BaseMatrixBinaryFunctionNode))]
public class BaseMatrixBinaryFunctionNodeView : BaseMatrixNodeView
{
    private       bool                         _showA;
    private       bool                         _showB;
    protected new BaseMatrixBinaryFunctionNode nodeTarget;

    public override void Enable()
    {
        base.Enable();
        nodeTarget = (BaseMatrixBinaryFunctionNode)target;
        SetupShowInputToggles();
    }

    private void SetupShowInputToggles()
    {
        var toggleShowA = new Toggle("Show A");
        var toggleShowB = new Toggle("Show B");

        toggleShowA.RegisterValueChangedCallback(value => _showA = value.newValue);
        toggleShowB.RegisterValueChangedCallback(value => _showB = value.newValue);

        controlsContainer.Add(toggleShowA);
        controlsContainer.Add(toggleShowB);
    }

    protected override void FillPreviewTexture(Matrix output, Texture2D texture)
    {
        var divider = GetColorDivider();

        for (var x = 0; x < output.width; x++)
        {
            for (var y = 0; y < output.height; y++)
            {
                var r = target.output[x, y] / divider;

                var g = nodeTarget.inputA?[x, y] ?? 0 / divider;
                var b = nodeTarget.inputB?[x, y] ?? 0 / divider;

                g = _showA ? g : 0;
                b = _showB ? b : 0;

                texture.SetPixel(x, y, new Color(r, g, b).gamma);
            }
        }
    }

    protected override float GetColorDivider()
    {
        return normalize
                   ? Matrix.GetMaximumOfAll(new[] { nodeTarget.inputA, nodeTarget.inputB, nodeTarget.output })
                   : 100f;
    }
}