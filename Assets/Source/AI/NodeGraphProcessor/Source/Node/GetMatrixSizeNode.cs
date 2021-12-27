using System;
using GraphProcessor;

[Serializable] [NodeMenuItem("Matrix/GetMatrixSizeNode")]
public class GetMatrixSizeNode : BaseNode
{
    [NodeOutput("Width")] [ShowAsDrawer]
    public int width;

    [NodeOutput("Height")] [ShowAsDrawer]
    public int height;

    [NodeInput("Matrix")]
    public Matrix inputMatrix;

    protected override void Process()
    {
        if (inputMatrix is null) return;

        width  = inputMatrix.width;
        height = inputMatrix.height;
    }
}