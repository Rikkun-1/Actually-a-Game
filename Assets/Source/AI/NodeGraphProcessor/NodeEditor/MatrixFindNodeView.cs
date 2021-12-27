using GraphProcessor;
using UnityEngine;

[NodeCustomEditor(typeof(MatrixFindNode))]
public class MatrixFindNodeView : BaseMatrixNodeView
{
    protected override void FillPreviewTexture(Matrix output, Texture2D texture)
    {
        var targetFindNode = (MatrixFindNode)nodeTarget;
        var funcX          = targetFindNode.x;
        var funcY          = targetFindNode.y;
        var value          = targetFindNode.value;
        var divider        = GetColorDivider();

        for (var x = 0; x < output.width; x++)
        {
            for (var y = 0; y < output.height; y++)
            {
                var r = x == funcX && y == funcY ? value : 0;
                texture.SetPixel(x, y, new Color(r, output[x, y] / divider, 0).gamma);
            }
        }
    }
}